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
            LogContext.Info?.Log("Audited Order: {OrderNumber}", context.Message.OrderNumber);
        }
    }
}