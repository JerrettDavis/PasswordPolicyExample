using System.Threading;
using PasswordPolicyExample.Models.Passwords;
using System.Threading.Tasks;

namespace PasswordPolicyExample.Business.Passwords
{
    public class PasswordPolicyProvider : IPasswordPolicyProvider
    {
        public PasswordPolicy GetPolicy()
        {
            return new PasswordPolicy
            {
                MinimumLength = 8,
                LowerCaseLength = 1,
                UpperCaseLength = 1,
                NumericLength = 1,
                NonAlphaLength = 1
            };
        }

        public Task<PasswordPolicy> GetPolicyAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(GetPolicy());
        }
    }
}
