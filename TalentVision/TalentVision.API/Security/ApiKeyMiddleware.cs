using System.Net;

namespace TalentVision.API.Security;

public class ApiKeyMiddleware
{
    private readonly RequestDelegate _next;
    private const string HeaderName = "X-Api-Key";

    public ApiKeyMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context, IConfiguration configuration)
    {
        var path = context.Request.Path.Value ?? string.Empty;

        if (path.StartsWith("/swagger") || path.StartsWith("/health"))
        {
            await _next(context);
            return;
        }

        string extractedApiKey = null;

        // 1) Checar header
        if (context.Request.Headers.TryGetValue(HeaderName, out var headerValue))
        {
            extractedApiKey = headerValue;
        }
        // 2) Se não veio no header, checar query string
        else if (context.Request.Query.TryGetValue("apiKey", out var queryValue))
        {
            extractedApiKey = queryValue;
        }

        if (extractedApiKey == null)
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("API Key não informada.");
            return;
        }

        var apiKey = configuration["ApiKey"];

        if (string.IsNullOrWhiteSpace(apiKey) || !apiKey.Equals(extractedApiKey))
        {
            context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            await context.Response.WriteAsync("API Key inválida.");
            return;
        }

        await _next(context);
    }
}