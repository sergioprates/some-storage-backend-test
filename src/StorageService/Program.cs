using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using StorageService;
using StorageService.Handlers;

public class Program
{
    public static void Main(string[] args)
    {
        CreateHostBuilder(args).Build().Run();
    }

    public static IHostBuilder CreateHostBuilder(string[] args) =>
        Host.CreateDefaultBuilder(args)
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHostedService<ConsumerBackgroundService>();
                services.AddSingleton<FileStorage>();

                services.AddMassTransit(x =>
                {
                    x.AddConsumer<EmailReadEventHandler>();

                    x.UsingRabbitMq((context, cfg) =>
                    {
                        cfg.ReceiveEndpoint(hostContext.Configuration["QueueName"]!, e =>
                        {
                            e.ConfigureConsumer<EmailReadEventHandler>(context);
                        });
                        cfg.ConfigureEndpoints(context);
                    });
                });
            });
}
