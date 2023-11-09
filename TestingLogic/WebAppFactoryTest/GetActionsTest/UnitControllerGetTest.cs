using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using webapi;

namespace TestingLogic.WebAppFactoryTest.GetActionsTest
{
    public class UnitControllerGetTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _fixture;
        private const string _notebookId = "d5beb573-64ce-4d54-23d7-08dbbe05ec67";
        private const string _unitId = "fd26b26d-5455-4504-898f-08dbbe05ec6b";
        private readonly Settings _settings = new Settings();

        public UnitControllerGetTest(WebApplicationFactory<Startup> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var reponse = await client.GetAsync($"{_settings.BaseAddress}/units?nbId={_notebookId}");
            Assert.Equal(HttpStatusCode.OK, reponse.StatusCode);
        }

        [Fact]
        public async Task GetAllNoAccessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.RonToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/units?nbId={_notebookId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAllDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/units?nbId={_notebookId}");
            var content = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task GetOneOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/units/{_unitId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOneNoAccessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.RonToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/units/{_unitId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetOneDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/units/{_unitId}");
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }
    }
}
