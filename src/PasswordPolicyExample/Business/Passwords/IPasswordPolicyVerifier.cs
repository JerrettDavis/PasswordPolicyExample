using System.Threading;
using System.Threading.Tasks;
using PasswordPolicyExample.Models.Responses;

namespace PasswordPolicyExample.Business.Passwords
{
    public interface IPasswordPolicyVerifier
    {
        IValidationResult Verify(string password);
        Task<IValidationResult> VerifyAsync(string password, CancellationToken cancellationToken = default);
    }
}