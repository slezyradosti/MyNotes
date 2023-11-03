using Application.Core;
using Microsoft.AspNetCore.Mvc;
using webapi.Controllers;

namespace TestingLogic
{
    public class BaseApiControllerTest : BaseApiController
    {
        [Fact]
        public void NullResultTest()
        {
            var expected = NotFound().ToString();
            var actual = HandleResult(Result<object>.Success(null)).ToString();

            Assert.Equal(expected, actual);
        }
    }
}
