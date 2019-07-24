using System.Collections.Generic;
using System.Linq;

namespace PasswordPolicyExample.Models.Responses.Passwords
{
    public class PasswordValidationResult : IValidationResult
    {
        public bool IsValid { get; }

        public IEnumerable<ErrorResponse> Errors { get; }

        public PasswordValidationResult(bool isValid)
        {
            IsValid = isValid;
            Errors = new List<ErrorResponse>();
        }

        public PasswordValidationResult(IEnumerable<ErrorResponse> errors)
        {
            Errors = errors ?? new List<ErrorResponse>();
            IsValid = !Errors.Any();
        }

        public PasswordValidationResult(bool isValid, IEnumerable<ErrorResponse> errors)
        {
            IsValid = isValid;
            Errors = errors ?? new List<ErrorResponse>();
        }
    }
}
