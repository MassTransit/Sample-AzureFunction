using System;
using MassTransit;
using MassTransit.ServiceBusIntegration;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Sample.AzureFunction;
using Sample.AzureFunction.Consumers;

[assembly: FunctionsStartup(typeof(Startup))]


namespace Sample.AzureFunction
{
    public class Startup :
        FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            builder.Services
                .AddScoped<SubmitOrderHttpFunction>()
                .AddMassTransit(x =>
                {
                    x.SetKebabCaseEndpointNameFormatter();
                    x.AddConsumersFromNamespaceContaining<ConsumerNamespace>();
                    x.AddRequestClient<SubmitOrder>(new Uri("queue:submit-order"));

                    x.UsingRabbitMq((context, cfg) => { cfg.ConfigureEndpoints(context); });
                })
                .AddSingleton<IAsyncBusHandle, AsyncBusHandle>()
                .RemoveMassTransitHostedService();
        }
    }
}