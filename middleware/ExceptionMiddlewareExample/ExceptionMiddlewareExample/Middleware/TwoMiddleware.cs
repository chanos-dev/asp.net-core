namespace ExceptionMiddlewareExample.Middleware
{
    public class TwoMiddleware
    {
        private readonly RequestDelegate _next;
        public TwoMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("Two Middleware");
            await _next(context);
        }

    }
}
