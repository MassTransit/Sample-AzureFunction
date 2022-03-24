using System;
using MassTransit;
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
                .AddScoped<SubmitOrderFunctions>()
                .AddScoped<AuditOrderFunctions>()
                .AddScoped<SubmitOrderHttpFunction>()
                .AddMassTransitForAzureFunctions(cfg =>
                    {
                        cfg.AddConsumersFromNamespaceContaining<ConsumerNamespace>();
                        cfg.AddRequestClient<SubmitOrder>(new Uri("queue:submit-order"));
                    },
                    "AzureWebJobsServiceBus")
                .AddMassTransitEventHub();
        }
    }
}