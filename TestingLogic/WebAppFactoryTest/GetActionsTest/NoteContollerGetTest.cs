using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using webapi;

namespace TestingLogic.WebAppFactoryTest.GetActionsTest
{
    public class NoteContollerGetTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _fixture;
        private const string _pageId = "5c401221-c080-4cba-95f9-08dbbe05ec6e";
        private const string _noteId = "e9dde2e9-753a-4958-f014-08dbbf9d9e50";

        public NoteContollerGetTest(WebApplicationFactory<Startup> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notes?pageId={_pageId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllNoAcessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.RonToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notes?pageId={_pageId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAllDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notes?pageId={_pageId}");
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task GetOneOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notes/{_noteId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOneNoAccessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.RonToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notes/{_noteId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetOneDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/notes/{_noteId}");
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }
    }
}
