using Microsoft.AspNetCore.RateLimiting;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddReverseProxy()
    .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddRateLimiter(opts =>
{
    opts.AddFixedWindowLimiter("fixed", o => //Max 5 requests per 10 seconds
    {
        o.Window = TimeSpan.FromSeconds(10);
        o.PermitLimit = 5;
    });
});

var app = builder.Build();

app.UseRateLimiter();
app.MapReverseProxy();

app.Run();
