using System.Diagnostics;

namespace SmartNeighborhoodAPI.Middlewares
{
    public class RequestTimingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestTimingMiddleware> _logger;
        private readonly int _thresholdMs;

        public RequestTimingMiddleware(RequestDelegate next, ILogger<RequestTimingMiddleware> logger, IConfiguration config)
        {
            _next = next;
            _logger = logger;
            _thresholdMs = config.GetValue<int>("Performance:SlowRequestThresholdMs", 1000);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var sw = Stopwatch.StartNew();
            await _next(context);
            sw.Stop();

            if (sw.ElapsedMilliseconds > _thresholdMs)
            {
                var method = context.Request.Method;
                var path = context.Request.Path;
                var ip = context.Connection.RemoteIpAddress?.ToString();

                _logger.LogWarning("Slow request detected: {Method} {Path} took {Duration}ms from IP {IP}",
                    method, path, sw.ElapsedMilliseconds, ip);
            }
        }
    }

}
