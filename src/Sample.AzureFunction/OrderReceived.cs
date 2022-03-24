using System;

namespace Sample.AzureFunction
{
    public interface OrderReceived
    {
        Guid OrderId { get; }
        DateTime Timestamp { get; }

        string OrderNumber { get; }
    }
}