using System.Threading;
using System.Threading.Tasks;
using MassTransit.WebJobs.ServiceBusIntegration;
using Microsoft.Azure.ServiceBus;
using Microsoft.Azure.WebJobs;
using Sample.AzureFunction.Consumers;

namespace Sample.AzureFunction
{
    public class SubmitOrderFunctions
    {
        const string SubmitOrderQueueName = "submit-order";
        readonly IMessageReceiver _receiver;

        public SubmitOrderFunctions(IMessageReceiver receiver)
        {
            _receiver = receiver;
        }

        [FunctionName("SubmitOrder")]
        public Task SubmitOrderAsync([ServiceBusTrigger(SubmitOrderQueueName)]
            Message message, CancellationToken cancellationToken)
        {
            return _receiver.HandleConsumer<SubmitOrderConsumer>(SubmitOrderQueueName, message, cancellationToken);
        }
    }
}
