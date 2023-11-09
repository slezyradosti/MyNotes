using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using System.Text;
using System.Text.Json;
using webapi;
using Xunit.Abstractions;

namespace TestingLogic.WebAppFactoryTest.GetActionsTest
{
    public class NotebookControllerGetTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _fixture;
        private readonly ITestOutputHelper _testOutputHelper;
        private const string _notebookId = "d5beb573-64ce-4d54-23d7-08dbbe05ec67";
        private readonly Settings _settings = new Settings();

        public NotebookControllerGetTest(WebApplicationFactory<Startup> fixture,
            ITestOutputHelper testOutputHelper)
        {
            _fixture = fixture;
            _testOutputHelper = testOutputHelper;
        }

        [Fact]
        public async Task GetAllOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/notebooks");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/notebooks");
            var content = await response.Content.ReadAsStringAsync();

            _testOutputHelper.WriteLine(content);

            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task GetOneOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/notebooks/{_notebookId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOneNoAccessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.RonToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/notebooks/{_notebookId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetOneDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/notebooks/{_notebookId}");
            var content = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(content);
        }
    }
}
