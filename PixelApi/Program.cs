using MassTransit;
using PixelApi.Infrastructure;
using PixelApi.Storages;
using StorageService.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<VisitorsService>();
builder.Services.AddScoped<RequestContext>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq();
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/track", async (HttpContext context, RequestContext requestContext, VisitorsService storageService, ILogger<Program> logger, CancellationToken cancellationToken) =>
{
    const string imagePixel = "iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==";

    try
    {
        var referrer = requestContext.Referer;
        var userAgent = requestContext.UserAgent;
        var ipAddress = requestContext.IpAddress;

        await storageService.SaveVisitorInfoAsync(new EmailReadEvent
        {
            Referrer = referrer,
            UserAgent = userAgent,
            IPAddress = ipAddress
        }, cancellationToken);

        context.Response.ContentType = "image/gif";
        await context.Response.WriteAsync(imagePixel);
    }
    catch (Exception e)
    {
        logger.LogError(e, "Something went wrong.");

        context.Response.ContentType = "image/gif";
        await context.Response.WriteAsync(imagePixel);
    }
})
.WithOpenApi();

app.Run();
