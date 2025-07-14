using Microsoft.AspNetCore.Server.HttpSys;

namespace Student_API_Project_v1.Middlewares
{
    public class RateLimitingMiddleware
    {
        private readonly RequestDelegate _next;
        private static int _counter = 0;
        private static DateTime _lastRequestTime = DateTime.Now;

        public RateLimitingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            _counter++;

            if (DateTime.Now.Subtract(_lastRequestTime).Seconds > 10)
            {
                _counter = 1;
                _lastRequestTime = DateTime.Now;
                await _next(context);
            }
            else
            {
                if (_counter > 5)
                {
                    _lastRequestTime = DateTime.Now;
                    context.Response.StatusCode = StatusCodes.Status429TooManyRequests;
                    await context.Response.WriteAsync("Rate limit exceeded. Please try again later.");
                }
                else
                {
                    _lastRequestTime = DateTime.Now;
                    await _next(context);
                }

            }


        }
    }
}
