using MassTransit;

namespace Sample.AzureFunction.Consumers
{
    public class SubmitOrderConsumerDefinition :
        ConsumerDefinition<SubmitOrderConsumer>
    {
        protected override void ConfigureConsumer(IReceiveEndpointConfigurator endpointConfigurator,
            IConsumerConfigurator<SubmitOrderConsumer> consumerConfigurator)
        {
            consumerConfigurator.UseMessageRetry(x => x.Intervals(10, 100, 500, 1000));
        }
    }
}