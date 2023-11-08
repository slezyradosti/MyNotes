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
        private readonly ITestOutputHelper _testOutputHelper;
        private const string _notebookId = "d5beb573-64ce-4d54-23d7-08dbbe05ec67";

        public NotebookControllerPostTest(CustomWebApplicatiionFactory fixture, ITestOutputHelper testOutputHelper)
        {
            _fixture = fixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task CallOrderedTests()
        {
            await RegisterTest();
            await LoginTest();
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

            using var response = await client.PostAsync($"{Settings.BaseAddress}/Account/register", jsonContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task LoginTest() // MUST BE IN A DIFFERENT CONTROLLER
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

            using var response = await client.PostAsync($"{Settings.BaseAddress}/Account/login", jsonContent);
            var content = await response.Content.ReadFromJsonAsync<UserDto>();
            Settings.userDto = content;

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task PostNotebookTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Settings.userDto.Token);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    id = new Guid().ToString(),
                    name = "TestUnitTest1"
                }),
                Encoding.UTF8,
                "application/json");

            using var response = await client.PostAsync($"{Settings.BaseAddress}/Notebooks/", jsonContent);

            //save created notebook for the following actions
            using var response2 = await client.GetAsync($"{Settings.BaseAddress}/Notebooks");
            var content = await response2.Content.ReadFromJsonAsync<List<NotebookDto>>();
            Settings.NotebookDto = content.FirstOrDefault();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task PutNotebookTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Settings.userDto.Token);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    name = "TestUnitTest1updated"
                }),
                Encoding.UTF8,
                "application/json");

            using var response = await client.PutAsync($"{Settings.BaseAddress}/Notebooks/{Settings.NotebookDto.Id}", jsonContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task DeleteNotebookTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + Settings.userDto.Token);

            using var response = await client.DeleteAsync($"{Settings.BaseAddress}/Notebooks/{Settings.NotebookDto.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}