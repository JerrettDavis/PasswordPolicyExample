namespace PasswordPolicyExample.Models.Responses
{
    public class ErrorResponse
    {
        public string Name { get; }
        public string Error { get; }

        public ErrorResponse(string name, string error)
        {
            Name = name;
            Error = error;
        }
    }
}
