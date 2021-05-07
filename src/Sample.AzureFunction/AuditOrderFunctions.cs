using System.Threading.Tasks;
using MassTransit.Transports;
using Microsoft.Azure.Functions.Worker;
using Sample.AzureFunction.Consumers;

namespace Sample.AzureFunction
{
    public class AuditOrderFunctions
    {
        public const string AuditOrderEventHubName = "audit-order";
        readonly IReceiveEndpointDispatcher<AuditOrderConsumer> _dispatcher;

        public AuditOrderFunctions(IReceiveEndpointDispatcher<AuditOrderConsumer> dispatcher)
        {
            _dispatcher = dispatcher;
        }

        [Function("AuditOrder")]
        public Task AuditOrderAsync([EventHubTrigger(AuditOrderEventHubName, Connection = "EventHubConnection", IsBatched = false)]
            byte[] body, FunctionContext context)
        {
            return _dispatcher.Dispatch(context, body);
        }
    }
}