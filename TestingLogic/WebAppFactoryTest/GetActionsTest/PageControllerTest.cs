using Azure;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using webapi;

namespace TestingLogic.WebAppFactoryTest.GetActionsTest
{
    public class PageControllerTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _fixture;
        private const string _unitId = "fd26b26d-5455-4504-898f-08dbbe05ec6b";
        private const string _pageId = "5c401221-c080-4cba-95f9-08dbbe05ec6e";

        public PageControllerTest(WebApplicationFactory<Startup> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/pages?unitId={_unitId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllNoAccessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.RonToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/pages?unitId={_unitId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAllDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/pages?unitId={_unitId}");
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task GetOneOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/pages/{_pageId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOneNoAccessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.RonToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/pages/{_pageId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetOneDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", Settings.JackToken);

            var response = await client.GetAsync($"{Settings.BaseAddress}/pages/{_pageId}");
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }
    }
}
