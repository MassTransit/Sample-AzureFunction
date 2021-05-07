using System.Threading.Tasks;
using MassTransit.Transports;
using Microsoft.Azure.Functions.Worker;
using Sample.AzureFunction.Consumers;

namespace Sample.AzureFunction
{
    public class SubmitOrderFunctions
    {
        const string SubmitOrderQueueName = "submit-order";
        readonly IReceiveEndpointDispatcher<SubmitOrderConsumer> _dispatcher;

        public SubmitOrderFunctions(IReceiveEndpointDispatcher<SubmitOrderConsumer> dispatcher)
        {
            _dispatcher = dispatcher;
            _dispatcher = dispatcher;
        }

        [Function("SubmitOrder")]
        public Task SubmitOrderAsync([ServiceBusTrigger(SubmitOrderQueueName, Connection = "ServiceBusConnection")]
            byte[] body, FunctionContext context)
        {
            return _dispatcher.Dispatch(context, body);
        }
    }
}