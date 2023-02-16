using ExceptionMiddlewareExample.Model;
using System.Net.Mime;

namespace ExceptionMiddlewareExample.Middleware
{
    public class ExceptionsMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionsMiddleware> _logger;

        public ExceptionsMiddleware(RequestDelegate next, ILogger<ExceptionsMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Exception Middleware");
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "error~");

                context.Response.ContentType = MediaTypeNames.Application.Json;
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                await context.Response.WriteAsJsonAsync(new ErrorData()
                {
                    Code = StatusCodes.Status500InternalServerError,
                    Message = ex.Message,
                    TraceID = $"{Guid.NewGuid():P}"
                });
            }
        }
    }
}
