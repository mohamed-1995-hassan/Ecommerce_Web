using Ecommerce_Web.Errors;
using System.Net;
using System.Text.Json;

namespace Ecommerce_Web.Middleware
{
    public class ExceptionMiddleware
    {
        public readonly RequestDelegate _next;
        public readonly ILogger<ExceptionMiddleware> _logger;
        public readonly IHostEnvironment _hostEnvironment;

        public ExceptionMiddleware(RequestDelegate next,
                                   ILogger<ExceptionMiddleware> logger,
                                   IHostEnvironment hostEnvironment)
        {
            _next = next;
            _logger = logger;
            _hostEnvironment = hostEnvironment;
        }

        public async Task Invoke(HttpContext context) 
        {
            try
            {
                await _next(context);
            }
            catch (Exception e) 
            {
                _logger.LogError(e, e.Message);
                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

                var response = _hostEnvironment.IsDevelopment() ?
                    new ApiException((int)HttpStatusCode.InternalServerError,
                                     e.Message,
                                     e.StackTrace.ToString()) :
                    new ApiResponse((int)HttpStatusCode.InternalServerError,
                                    e.Message);

                var option = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                };

                var json = JsonSerializer.Serialize(response, option);

                await context.Response.WriteAsync(json);
            }
        }
    }
}
