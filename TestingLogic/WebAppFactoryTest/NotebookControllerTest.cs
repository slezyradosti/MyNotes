using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using webapi;
using Xunit.Abstractions;

namespace TestingLogic.WebAppFactoryTest
{
    public class NotebookControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _fixture;
        private readonly ITestOutputHelper _testOutputHelper;
        private const string _notebookId = "d5beb573-64ce-4d54-23d7-08dbbe05ec67";

        public NotebookControllerTest(WebApplicationFactory<Startup> fixture,
            ITestOutputHelper testOutputHelper)
        {
            _fixture = fixture;
            _testOutputHelper = testOutputHelper;
            Settings.JackToken = "Bearer eyJhbGciOiJIUzUxMiIsInR5cCI6IkpXVCJ9.eyJ1bmlxdWVfbmFtZSI6ImphY2siLCJuYW1laWQiOiI4MjI0NTc4NC05NjFmLTRlNjYtNTFkMC0wOGRiYmUwNWVjMjgiLCJlbWFpbCI6ImphY2tAdGFjay5jb20iLCJuYmYiOjE2OTkyNjUzMTQsImV4cCI6MTY5OTg3MDExNCwiaWF0IjoxNjk5MjY1MzE0fQ.f_8iUnCzQ4JCEss_lz6chdk0t74l2l3U9fIQngT38SBB_6ADDG5fBCiuXxYTY3weEpRnzStshQJskU9h51zAXw";
        }

        [Fact]
        public async Task GetAllOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notebooks");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notebooks");
            var content = await response.Content.ReadAsStringAsync();

            _testOutputHelper.WriteLine(content);

            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task GetOneOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notebooks/{_notebookId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOneNoAccessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.RonToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notebooks/{_notebookId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetOneDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notebooks/{_notebookId}");
            var content = await response.Content.ReadAsStringAsync();

            Assert.NotEmpty(content);
        }
    }
}
