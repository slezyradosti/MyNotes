using Domain.Models;
using Domain.Repositories.EFInitial;
using Domain.Repositories.Repos;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Text;
using System.Text.Json;
using Xunit.Abstractions;

namespace TestingLogic.WebAppFactoryTest.GetActionsTest
{
    public class NotebookControllerGetTest : IClassFixture<CustomWebApplicatiionFactory>
    {
        private readonly CustomWebApplicatiionFactory _fixture;
        private readonly ITestOutputHelper _testOutputHelper;
        private const string _notebookId = "d5beb573-64ce-4d54-23d7-08dbbe05ec67";

        public NotebookControllerGetTest(CustomWebApplicatiionFactory fixture,
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

        //summary
        //using in-memory databse
        //summary
        [Fact]
        public async Task PostRangeTest()
        {
            //var connection = new SqliteConnection("DataSource=:memory:");
            //connection.Open();

            //var options = new DbContextOptionsBuilder<DataContext>()
            //    .UseSqlite(connection)
            //    .Options;

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





            //using (var context = new DataContext(options))
            //{
            //    context.Database.EnsureCreated();

            //    var notebookRepo = new NotebookRepository(context);

            //    //await notebookRepo.AddRangeAsync(new List<Notebook> {
            //    //   new Notebook { Name = "TestNotebook1", Author = author, Timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks) },
            //    //   new Notebook { Name = "TestNotebook2", Author = author, Timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks) },
            //    //   new Notebook { Name = "TestNotebook3", Author = author, Timestamp = BitConverter.GetBytes(DateTime.UtcNow.Ticks) }
            //    //   });

            //    var author = context.Users.First();

            //    context.Notebooks.AddRange(
            //        new Notebook { Name = "TestNotebook1", Author = author },
            //        new Notebook { Name = "TestNotebook2", Author = author },
            //        new Notebook { Name = "TestNotebook3", Author = author }
            //        );
            //    context.SaveChanges();
            //}

            //using (var context = new DataContext(options))
            //{
            //    var notebookRepo = new NotebookRepository(context);

            //    var notebooks = await notebookRepo.GetAllAsync();

            //    Assert.NotNull(notebooks);
            //    Assert.Equal("TestNotebook1", notebooks.FirstOrDefault(x => x.Name == "TestNotebook1").Name);
            //}

            //connection.Close();
        }
    }
}
