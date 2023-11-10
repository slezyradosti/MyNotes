using Application.DTOs;
using IndentityLogic.DTOs;
using System.Net;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace TestingLogic.WebAppFactoryTest.PostActionsTest
{
    public class NotebookControllerPostTest : IClassFixture<CustomWebApplicatiionFactory>
    {
        private readonly CustomWebApplicatiionFactory _fixture;
        private readonly Settings _settings = new Settings();

        public NotebookControllerPostTest(CustomWebApplicatiionFactory fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CallOrderedTests()
        {
            await RegisterTest();
            await PostNotebookTest();
            await PutNotebookTest();
            await DeleteNotebookTest();
        }

        /// <summary>
        /// using in-memory databse
        /// </summary>

        private async Task RegisterTest() // MUST BE IN A DIFFERENT CONTROLLER
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
            var content = await response.Content.ReadFromJsonAsync<UserDto>();
            _settings.userDto = content;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task PostNotebookTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _settings.userDto.Token);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    id = new Guid().ToString(),
                    name = "TestUnitTest1"
                }),
                Encoding.UTF8,
                "application/json");

            using var response = await client.PostAsync($"{_settings.BaseAddress}/Notebooks/", jsonContent);

            //save created notebook for the following actions
            using var response2 = await client.GetAsync($"{_settings.BaseAddress}/Notebooks");
            var content = await response2.Content.ReadFromJsonAsync<List<NotebookDto>>();
            _settings.NotebookDto = content.FirstOrDefault();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task PutNotebookTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _settings.userDto.Token);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    name = "TestUnitTest1updated"
                }),
                Encoding.UTF8,
                "application/json");

            using var response = await client.PutAsync($"{_settings.BaseAddress}/Notebooks/{_settings.NotebookDto.Id}", jsonContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task DeleteNotebookTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _settings.userDto.Token);

            using var response = await client.DeleteAsync($"{_settings.BaseAddress}/Notebooks/{_settings.NotebookDto.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}