using Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace Infrastructure.Middleware;

public class ResponseLoggingMiddleware : IMiddleware
{
    private readonly ICurrentUser _currentUser;

    public ResponseLoggingMiddleware(ICurrentUser currentUser) => _currentUser = currentUser;

    public async Task InvokeAsync(HttpContext httpContext, RequestDelegate next)
    {
        await next(httpContext);
        var originalBody = httpContext.Response.Body;
        using var newBody = new MemoryStream();
        httpContext.Response.Body = newBody;
        string responseBody;
        if (httpContext.Request.Path.ToString().Contains("tokens"))
        {
            responseBody = "[Redacted] Contains Sensitive Information.";
        }
        else
        {
            newBody.Seek(0, SeekOrigin.Begin);
            responseBody = await new StreamReader(httpContext.Response.Body).ReadToEndAsync();
        }

        string email = _currentUser.GetUserEmail() is string userEmail ? userEmail : "Anonymous";
        var userId = _currentUser.GetUserId();
        string tenant = _currentUser.GetTenant() ?? string.Empty;
        newBody.Seek(0, SeekOrigin.Begin);
        await newBody.CopyToAsync(originalBody);
    }
}