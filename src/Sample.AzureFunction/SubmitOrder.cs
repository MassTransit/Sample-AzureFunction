using System;

namespace Sample.AzureFunction
{
    public interface SubmitOrder
    {
        Guid OrderId { get; }
        string OrderNumber { get; }
    }
}