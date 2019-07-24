using PasswordPolicyExample.Models.Passwords;
using PasswordPolicyExample.Models.Responses;
using PasswordPolicyExample.Models.Responses.Passwords;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace PasswordPolicyExample.Business.Passwords
{
    public class PasswordPolicyVerifier : IPasswordPolicyVerifier
    {
        private delegate (bool isValid, ErrorResponse error) VerificationMethod(string password);

        private readonly IPasswordPolicyProvider _policyProvider;
        private PasswordPolicy _policy;

        public PasswordPolicyVerifier(IPasswordPolicyProvider policyProvider)
        {
            _policyProvider = policyProvider;
        }

        public IValidationResult Verify(string password)
        {
            _policy = _policyProvider.GetPolicy();
            return RunVerification(password);
        }

        public async Task<IValidationResult> VerifyAsync(string password, CancellationToken cancellationToken = default)
        {
            _policy = await _policyProvider.GetPolicyAsync(cancellationToken);
            return RunVerification(password);
        }

        private IValidationResult RunVerification(string password)
        {
            var isValid = true;
            var errors = new List<ErrorResponse>();
            VerificationMethod[] verifications =
            {
                VerifyUpperCase,
                VerifyMinimumLength,
                VerifyLowerCase,
                VerifyNonAlpha,
                VerifyNumeric
            };
            foreach (var verification in verifications)
            {
                var (isPasswordValid, error) = verification(password);
                if (!isPasswordValid)
                    isValid = false;
                if (error != null)
                    errors.Add(error);
            }

            return new PasswordValidationResult(isValid, errors);
        }

        private (bool, ErrorResponse) VerifyUpperCase(string password)
        {
            var enoughUpper = Regex.Matches(password, "[A-Z]").Count >= _policy.UpperCaseLength;
            return !enoughUpper ?
                (false, new ErrorResponse("UpperCaseCount", $"Password does not meet the upper case character count requirement set by the password policy ({ _policy.UpperCaseLength})"))
                : (true, null);
        }

        private (bool, ErrorResponse) VerifyMinimumLength(string password)
        {
            var longEnough = password.Length >= _policy.MinimumLength;
            return !longEnough ?
                (false, new ErrorResponse("MinimumLength", $"Password does not meet the minimum length requirement set by the password policy ({ _policy.MinimumLength})"))
                : (true, null);
        }

        private (bool, ErrorResponse) VerifyLowerCase(string password)
        {
            var enoughLower = Regex.Matches(password, "[a-z]").Count >= _policy.LowerCaseLength;
            return !enoughLower ?
                (false, new ErrorResponse("LowerCaseCount", $"Password does not meet the lower case character count requirement set by the password policy ({ _policy.LowerCaseLength})"))
                : (true, null);
        }

        private (bool, ErrorResponse) VerifyNonAlpha(string password)
        {
            var enoughNonAlpha = Regex.Matches(password, @"[^0-9a-zA-Z\._]").Count >= _policy.NonAlphaLength;
            return !enoughNonAlpha ?
                (false, new ErrorResponse("NonAlphaCount", $"Password does not meet the special character count requirement set by the password policy ({ _policy.NonAlphaLength})"))
                : (true, null);
        }

        private (bool, ErrorResponse) VerifyNumeric(string password)
        {
            var enoughNumeric = Regex.Matches(password, @"[0-9]").Count >= _policy.NumericLength;
            return !enoughNumeric ?
                (false, new ErrorResponse("NumericCount", $"Password does not meet the numeric character count requirement set by the password policy ({_policy.NumericLength})"))
                : (true, null);
        }
    }
}
