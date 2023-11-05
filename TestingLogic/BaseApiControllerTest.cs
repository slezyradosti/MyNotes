using Application.Core;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using webapi.Controllers;

namespace TestingLogic
{
    public class BaseApiControllerTest : BaseApiController
    {
        [Fact]
        public void NullRequestTest()
        {
            var actual = HandleResult(Result<object>.Success(null));

            Assert.IsType<NotFoundResult>(actual);
        }
        
        [Fact]
        public void FailRequestTest()
        {
            var res = Result<object>.Failure(null);
            var actual = HandleResult(res);

            Assert.IsType<BadRequestObjectResult>(actual);
        }

        [Fact]
        public void SuccessReqeustTest()
        {
            var res = Result<object>.Success(1);
            var actual = HandleResult(res);

            Assert.IsType<OkObjectResult>(actual);
        }
    }
}
