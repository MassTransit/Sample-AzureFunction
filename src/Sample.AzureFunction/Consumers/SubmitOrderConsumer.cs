using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Context;
using MassTransit.EventHubIntegration;

namespace Sample.AzureFunction.Consumers
{
    public class SubmitOrderConsumer :
        IConsumer<SubmitOrder>
    {
        readonly IEventHubProducerProvider _producerProvider;

        public SubmitOrderConsumer(IEventHubProducerProvider producerProvider)
        {
            _producerProvider = producerProvider;
        }

        public async Task Consume(ConsumeContext<SubmitOrder> context)
        {
            LogContext.Info?.Log("Processing Order: {OrderNumber}", context.Message.OrderNumber);

            var producer = await _producerProvider.GetProducer(AuditOrderFunctions.AuditOrderEventHubName);

            await Task.WhenAll(
                producer.Produce<OrderReceived>(new
                {
                    context.Message.OrderNumber,
                    Timestamp = DateTime.UtcNow
                }),
                context.Publish<OrderReceived>(new
                {
                    context.Message.OrderNumber,
                    Timestamp = DateTime.UtcNow
                }),
                context.RespondAsync<OrderAccepted>(new {context.Message.OrderNumber}));
        }
    }
}