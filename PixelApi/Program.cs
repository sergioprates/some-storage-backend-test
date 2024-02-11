using MassTransit;
using PixelApi.Storages;
using StorageService.Events;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<VisitorsService>();

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

app.MapGet("/track", async (HttpContext context, VisitorsService storageService, CancellationToken cancellationToken) =>
{
    var referrer = context.Request.Headers["Referer"].ToString();
    var userAgent = context.Request.Headers["User-Agent"].ToString();
    var ipAddress = context.Connection.RemoteIpAddress?.ToString();

    await storageService.SaveVisitorInfoAsync(new EmailReadEvent
    {
        Referrer = referrer,
        UserAgent = userAgent,
        IPAddress = ipAddress
    }, cancellationToken);

    context.Response.ContentType = "image/gif";
    await context.Response.WriteAsync("iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAYAAAAfFcSJAAAADUlEQVR42mNkYPhfDwAChwGA60e6kgAAAABJRU5ErkJggg==");
})
.WithOpenApi();

app.Run();
