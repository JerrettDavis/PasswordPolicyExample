using System.Threading;
using System.Threading.Tasks;
using PasswordPolicyExample.Models.Passwords;

namespace PasswordPolicyExample.Business.Passwords
{
    public interface IPasswordPolicyProvider
    {
        PasswordPolicy GetPolicy();
        Task<PasswordPolicy> GetPolicyAsync(CancellationToken cancellationToken = default);
    }
}