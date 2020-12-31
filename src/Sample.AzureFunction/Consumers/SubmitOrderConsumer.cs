using System;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Context;

namespace Sample.AzureFunction.Consumers
{
    public class SubmitOrderConsumer :
        IConsumer<SubmitOrder>
    {
        public Task Consume(ConsumeContext<SubmitOrder> context)
        {
            LogContext.Debug?.Log("Processing Order: {OrderNumber}", context.Message.OrderNumber);

            context.Publish<OrderReceived>(new
            {
                context.Message.OrderNumber,
                Timestamp = DateTime.UtcNow
            });

            return context.RespondAsync<OrderAccepted>(new {context.Message.OrderNumber});
        }
    }
}
