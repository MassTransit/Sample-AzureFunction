using System.Threading.Tasks;
using MassTransit;
using MassTransit.Context;

namespace Sample.AzureFunction.Consumers
{
    public class AuditOrderConsumer :
        IConsumer<OrderReceived>
    {
        public async Task Consume(ConsumeContext<OrderReceived> context)
        {
            LogContext.Debug?.Log("Received Order: {OrderNumber}", context.Message.OrderNumber);
        }
    }
}
