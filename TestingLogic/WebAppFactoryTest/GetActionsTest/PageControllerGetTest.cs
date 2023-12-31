﻿using Azure;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;
using webapi;

namespace TestingLogic.WebAppFactoryTest.GetActionsTest
{
    public class PageControllerGetTest : IClassFixture<WebApplicationFactory<Startup>>
    {
        private readonly WebApplicationFactory<Startup> _fixture;
        private const string _unitId = "fd26b26d-5455-4504-898f-08dbbe05ec6b";
        private const string _pageId = "5c401221-c080-4cba-95f9-08dbbe05ec6e";
        private readonly Settings _settings = new Settings();

        public PageControllerGetTest(WebApplicationFactory<Startup> fixture)
        {
            _fixture = fixture;
        }

        [Fact]
        public async Task GetAllOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/pages?unitId={_unitId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetAllNoAccessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.RonToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/pages?unitId={_unitId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetAllDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/pages?unitId={_unitId}");
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }

        [Fact]
        public async Task GetOneOkTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/pages/{_pageId}");
            Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        }

        [Fact]
        public async Task GetOneNoAccessTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.RonToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/pages/{_pageId}");
            Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
        }

        [Fact]
        public async Task GetOneDataTest()
        {
            HttpClient client = _fixture.CreateClient();
            client.DefaultRequestHeaders.Add("Authorization", _settings.JackToken);

            var response = await client.GetAsync($"{_settings.BaseAddress}/pages/{_pageId}");
            var content = await response.Content.ReadAsStringAsync();
            Assert.NotEmpty(content);
        }
    }
}
