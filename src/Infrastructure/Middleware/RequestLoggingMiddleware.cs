using System.Text;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middleware;

public class RequestLoggingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        string requestBody = string.Empty;
        if (httpContext.Request.Path.ToString().Contains("tokens"))
        {
            requestBody = "[Redacted] Contains Sensitive Information.";
        }
        else
        {
            var request = httpContext.Request;

            if (!string.IsNullOrEmpty(request.ContentType)
                && request.ContentType.StartsWith("application/json"))
            {
                request.EnableBuffering();
                using var reader = new StreamReader(request.Body, Encoding.UTF8, true, 4096, true);
                requestBody = await reader.ReadToEndAsync();

                // rewind for next middleware.
                request.Body.Position = 0;
            }
        }

        await next(httpContext);
    }
}