using IndentityLogic.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace TestingLogic.WebAppFactoryTest.PostActionsTest
{
    public class AccountControllerPostTest : IClassFixture<CustomWebApplicatiionFactory>
    {
        private readonly CustomWebApplicatiionFactory _fixture;
        private readonly Settings _settings = new Settings();

        public AccountControllerPostTest(CustomWebApplicatiionFactory fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CallOrderedTests()
        {
            await RegisterTest();
            await LoginTest();
        }
        private async Task RegisterTest()
        {
            HttpClient client = _fixture.CreateClient();

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    email = "mona@test.com",
                    username = "mona",
                    displayname = "Mona",
                    password = "Pa$$w0rd"
                }),
                Encoding.UTF8,
                "application/json");

            using var response = await client.PostAsync($"{_settings.BaseAddress}/Account/register", jsonContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task LoginTest()
        {
            HttpClient client = _fixture.CreateClient();

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    email = "mona@test.com",
                    password = "Pa$$w0rd"
                }),
                Encoding.UTF8,
                "application/json");

            using var response = await client.PostAsync($"{_settings.BaseAddress}/Account/login", jsonContent);
            var content = await response.Content.ReadFromJsonAsync<UserDto>();
            _settings.userDto = content;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
