using PasswordPolicyExample.Business.Passwords;
using System.Threading.Tasks;
using Xunit;

namespace PasswordPolicyExample.Tests.Business.Passwords
{
    public class PasswordPolicyProviderTests
    {
        [Fact]
        public void GetPolicyTest_ShouldSucceed()
        {
            var provider = new PasswordPolicyProvider();
            var result = provider.GetPolicy();

            Assert.NotNull(result);
            Assert.True(result.MinimumLength > 0);
        }

        [Fact]
        public async Task GetPolicyAsyncTest_ShouldSucceed()
        {
            var provider = new PasswordPolicyProvider();
            var result = await provider.GetPolicyAsync();

            Assert.NotNull(result);
            Assert.True(result.MinimumLength > 0);
        }
    }
}
