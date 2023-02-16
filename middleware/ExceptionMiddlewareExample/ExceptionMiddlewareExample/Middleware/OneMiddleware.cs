namespace ExceptionMiddlewareExample.Middleware
{
    public class OneMiddleware
    {
        private readonly RequestDelegate _next;
        public OneMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Console.WriteLine("One Middleware");
            await _next(context);
        }

    }
}
