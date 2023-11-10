using Application.DTOs;
using IndentityLogic.DTOs;
using System.Net.Http.Json;
using System.Net;
using System.Text;
using System.Text.Json;

namespace TestingLogic.WebAppFactoryTest.PostActionsTest
{
    public class PageControllerPostTest : IClassFixture<CustomWebApplicatiionFactory>
    {
        private readonly CustomWebApplicatiionFactory _fixture;
        private readonly Settings _settings = new Settings();

        public PageControllerPostTest(CustomWebApplicatiionFactory fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task CallOrderedTests()
        {
            await RegisterTest();
            await PostNotebook();
            await PostUnit();
            await PostPageTest();
            await PutPageTest();
            await DeletePageTest();
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

        // as unit cannot exist without notebook, we need to create notebook first
        private async Task PostNotebook()
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
        }

        private async Task PostUnit()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _settings.userDto.Token);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    name = "TestUnitTest1",
                    NotebookId = _settings.NotebookDto.Id.ToString(),
                }),
                Encoding.UTF8,
                "application/json");

            using var response = await client.PostAsync($"{_settings.BaseAddress}/Units/", jsonContent);

            //save created notebook for the following actions
            using var response2 = await client.GetAsync($"{_settings.BaseAddress}/Units?nbId={_settings.NotebookDto.Id}");
            var content = await response2.Content.ReadFromJsonAsync<List<UnitDto>>();
            _settings.UnitDto = content.FirstOrDefault();
        }

        private async Task PostPageTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _settings.userDto.Token);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    name = "TestPage1",
                    UnitId = _settings.UnitDto.Id.ToString()
                }),
                Encoding.UTF8,
                "application/json");

            using var response = await client.PostAsync($"{_settings.BaseAddress}/Pages/", jsonContent);

            //save created notebook for the following actions
            using var response2 = await client.GetAsync($"{_settings.BaseAddress}/Pages?unitId={_settings.UnitDto.Id}");
            var content = await response2.Content.ReadFromJsonAsync<List<PageDto>>();
            _settings.PageDto = content.FirstOrDefault();

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task PutPageTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _settings.userDto.Token);

            using StringContent jsonContent = new(
                JsonSerializer.Serialize(new
                {
                    name = "TestUnitTest1updated",
                    UnitId = _settings.UnitDto.Id.ToString()
                }),
                Encoding.UTF8,
                "application/json");

            using var response = await client.PutAsync($"{_settings.BaseAddress}/Pages/{_settings.PageDto.Id}", jsonContent);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        private async Task DeletePageTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + _settings.userDto.Token);

            using var response = await client.DeleteAsync($"{_settings.BaseAddress}/Pages/{_settings.PageDto.Id}");

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }
    }
}
