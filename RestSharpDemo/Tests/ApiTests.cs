using NUnit.Framework;
using System.Threading.Tasks;

namespace RestSharpDemo.Tests
{
    public class ApiTests
    {
        private static ApiClient? api;

        [OneTimeSetUp]
        public async Task GlobalSetup()
        {
            api = new ApiClient();
            var authSuccess = await api.AuthenticateAsync();
            Assert.That(authSuccess, Is.True, "❌ Authentication failed in GlobalSetup.");
        }

        [Test]
        public async Task Authentication_ShouldSucceed()
        {
            var tempApi = new ApiClient();
            var result = await tempApi.AuthenticateAsync();
            Assert.That(result, Is.True, "❌ Authentication failed.");
        }

        [Test]
        public async Task GetProductListApi_ShouldReturnSuccess()
        {
            var result = await api!.GetProductListAsync();
            Assert.That(result, Is.True, "❌ Product list fetch failed.");
        }

        [Test]
        public async Task GetProductManifest_ShouldReturnSuccess()
        {
            var response = await api!.GetProductManifestAsync();
            Assert.That(response.IsSuccessful, Is.True, "❌ Manifest API call failed.");
            Assert.That(response.Content, Is.Not.Null.And.Not.Empty, "❌ Manifest content was empty.");
        }
    }
}
