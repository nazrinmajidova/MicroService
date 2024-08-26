using HangFireApplication.MqServices;
using MassTransit;
using System.Runtime.CompilerServices;

namespace HangFireApplication.Configuration;

public static class RabbitMqConfiguration
{
    public static void ConfigureRabbitMq(this IServiceCollection services, IConfiguration configuration)
    {
        var rabbitMqConfig = configuration
            .GetSection("RabbitMq")
            .Get<RabbitMqConfig>();

        services.AddSingleton(rabbitMqConfig);

        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(rabbitMqConfig.Host, h =>
                {
                    h.Username(rabbitMqConfig.Username);
                    h.Password(rabbitMqConfig.Password);
                });

                cfg.ReceiveEndpoint("hellojob-queue", e =>
                {
                    //e.Consumer<JobSearchConsumer>(context);
                });

                cfg.ReceiveEndpoint("jobsearch-queue", e =>
                {
                    //e.Consumer<JobSearchConsumer>(context);
                });
                cfg.ReceiveEndpoint("boss-az-queue", e =>
                {
                    //e.Consumer<JobSearchConsumer>(context);
                });
            });
        });


        services.AddHostedService<RabbitMqHostedService>();
    }
}


public class RabbitMqConfig
{
    public string Host { get; set; }
    public int Port { get; set; } = 5672; //default port
    public string Username { get; set; }  //default Username -> guest
    public string Password { get; set; }  //default Password -> guest
    public string VirtualHost { get; set; } // -> "/" 
}
