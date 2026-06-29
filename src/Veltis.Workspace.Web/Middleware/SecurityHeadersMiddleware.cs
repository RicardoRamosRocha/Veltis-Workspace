namespace Veltis.Workspace.Web.Middleware;

public sealed class SecurityHeadersMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IWebHostEnvironment _environment;

    public SecurityHeadersMiddleware(RequestDelegate next, IWebHostEnvironment environment)
    {
        _next = next;
        _environment = environment;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        IHeaderDictionary headers = context.Response.Headers;
        headers.TryAdd("X-Content-Type-Options", "nosniff");
        headers.TryAdd("X-Frame-Options", "DENY");
        headers.TryAdd("Referrer-Policy", "strict-origin-when-cross-origin");
        headers.TryAdd("Permissions-Policy", "camera=(), microphone=(), geolocation=()");

        string scriptSource = _environment.IsDevelopment()
            ? "'self' 'unsafe-inline' https://cdn.tailwindcss.com"
            : "'self' https://cdn.tailwindcss.com";

        headers.TryAdd(
            "Content-Security-Policy",
            $"default-src 'self'; script-src {scriptSource}; style-src 'self' 'unsafe-inline'; img-src 'self' data:; font-src 'self'; connect-src 'self'; frame-ancestors 'none';");

        await _next(context);
    }
}
