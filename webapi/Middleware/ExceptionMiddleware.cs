using Application.Core;
using System.Net;
using System.Text.Json;

namespace webapi.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _requestDelegate;
        private readonly ILogger<ExceptionMiddleware> _logger;
        private readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddleware(RequestDelegate requestDelegate, ILogger<ExceptionMiddleware> logger,
            IHostEnvironment hostEnvironment)
        {
            _requestDelegate = requestDelegate;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _requestDelegate(httpContext);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                //as we outside of controller
                httpContext.Response.ContentType = "application/json";
                httpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                Console.WriteLine(ex.InnerException?.Message);

                var response = _hostEnvironment.IsDevelopment()
                    ? new AppException(httpContext.Response.StatusCode, ex.Message, ex.StackTrace?.ToString())
                    : new AppException(httpContext.Response.StatusCode, "Internal sever error");

                //as we outside of controller
                var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

                var json = JsonSerializer.Serialize(response, options);

                await httpContext.Response.WriteAsync(json);
            }
        }
    }
}
