using System.Collections.Generic;

namespace PasswordPolicyExample.Models.Responses
{
    public interface IValidationResult
    {
        bool IsValid { get; }
        IEnumerable<ErrorResponse> Errors { get; }
    }
}
