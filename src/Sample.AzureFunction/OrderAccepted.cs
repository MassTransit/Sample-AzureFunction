using System;

namespace Sample.AzureFunction
{
    public interface OrderAccepted
    {
        Guid OrderId { get; }
        string OrderNumber { get; }
    }
}
