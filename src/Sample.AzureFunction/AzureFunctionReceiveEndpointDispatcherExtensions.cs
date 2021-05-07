using System;
using System.Threading;
using System.Threading.Tasks;
using MassTransit.Transports;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;

namespace Sample.AzureFunction
{
    public static class AzureFunctionReceiveEndpointDispatcherExtensions
    {
        public static Task Dispatch(this IReceiveEndpointDispatcher dispatcher, FunctionContext context, byte[] body,
            CancellationToken cancellationToken = default)
        {
            return dispatcher.Dispatch(body, context.BindingContext.BindingData, cancellationToken,
                context.InstanceServices, new FunctionScope(context.InstanceServices));
        }

        class FunctionScope :
            IServiceScope
        {
            public FunctionScope(IServiceProvider serviceProvider)
            {
                ServiceProvider = serviceProvider;
            }

            public void Dispose()
            {
            }

            public IServiceProvider ServiceProvider { get; }
        }
    }
}