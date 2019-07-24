namespace PasswordPolicyExample.Models.Passwords
{
    public class PasswordPolicy
    {
        public int MinimumLength { get; set; }
        public int UpperCaseLength { get; set; }
        public int LowerCaseLength { get; set; }
        public int NonAlphaLength { get; set; }
        public int NumericLength { get; set; }
    }
}
