using Moq;
using PasswordPolicyExample.Business.Passwords;
using PasswordPolicyExample.Models.Passwords;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace PasswordPolicyExample.Tests.Business.Passwords
{
    public class PasswordPolicyVerifierTests
    {
        private readonly IPasswordPolicyVerifier _verifier;

        public PasswordPolicyVerifierTests()
        {
            var providerMock = new Mock<IPasswordPolicyProvider>();
            var policy = new PasswordPolicy
            {
                MinimumLength = 7,
                LowerCaseLength = 1,
                NonAlphaLength = 1,
                NumericLength = 1,
                UpperCaseLength = 1
            };
            providerMock.Setup(n => n.GetPolicy())
                .Returns(policy);
            providerMock.Setup(n => n.GetPolicyAsync(It.IsAny<CancellationToken>()))
                .ReturnsAsync(policy);
            _verifier = new PasswordPolicyVerifier(providerMock.Object);
        }
        [Fact]
        public void VerifyTest_ShouldSucceed()
        {
            var testPassword = "@Test12345";
            var result = _verifier.Verify(testPassword);

            Assert.True(result.IsValid);
        }

        [Fact]
        public async Task VerifyAsyncTest_ShouldSucceed()
        {
            var testPassword = "@Test12345";
            var result = await _verifier.VerifyAsync(testPassword);

            Assert.True(result.IsValid);
        }

        [Fact]
        public void VerifyTest_TooShort_ShouldFail()
        {
            var testPassword = "@Te1";
            var result = _verifier.Verify(testPassword);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public async Task VerifyAsyncTest_TooShort_ShouldFail()
        {
            var testPassword = "@Te1";
            var result = await _verifier.VerifyAsync(testPassword);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }
        
        [Fact]
        public void VerifyTest_NoUpper_ShouldFail()
        {
            var testPassword = "@test12345";
            var result = _verifier.Verify(testPassword);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public async Task VerifyAsyncTest_NoUpper_ShouldFail()
        {
            var testPassword = "@test12345";
            var result = await _verifier.VerifyAsync(testPassword);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public void VerifyTest_NoLower_ShouldFail()
        {
            var testPassword = "@TEST12345";
            var result = _verifier.Verify(testPassword);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public async Task VerifyAsyncTest_NoLower_ShouldFail()
        {
            var testPassword = "@TEST12345";
            var result = await _verifier.VerifyAsync(testPassword);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public void VerifyTest_NoSpecial_ShouldFail()
        {
            var testPassword = "Test12345";
            var result = _verifier.Verify(testPassword);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }

        [Fact]
        public async Task VerifyAsyncTest_NoSpecial_ShouldFail()
        {
            var testPassword = "Test12345";
            var result = await _verifier.VerifyAsync(testPassword);

            Assert.False(result.IsValid);
            Assert.Single(result.Errors);
        }
    }
}
