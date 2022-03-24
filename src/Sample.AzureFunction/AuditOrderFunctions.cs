using System.Threading;
using System.Threading.Tasks;
using Azure.Messaging.EventHubs;
using MassTransit;
using Microsoft.Azure.WebJobs;
using Sample.AzureFunction.Consumers;

namespace Sample.AzureFunction
{
    public class AuditOrderFunctions
    {
        const string AuditOrderEventHubName = "audit-order";
        readonly IEventReceiver _receiver;

        public AuditOrderFunctions(IEventReceiver receiver)
        {
            _receiver = receiver;
        }

        [FunctionName("AuditOrder")]
        public Task AuditOrderAsync([EventHubTrigger(AuditOrderEventHubName, Connection = "AzureWebJobsEventHub")] EventData message,
            CancellationToken cancellationToken)
        {
            return _receiver.HandleConsumer<AuditOrderConsumer>(AuditOrderEventHubName, message, cancellationToken);
        }
    }
}