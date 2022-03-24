using System.Text.Json;
using System.Threading.Tasks;
using MassTransit;
using MassTransit.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace Sample.AzureFunction
{
    public class SubmitOrderHttpFunction
    {
        readonly IRequestClient<SubmitOrder> _client;
        readonly IAsyncBusHandle _handle;

        public SubmitOrderHttpFunction(IAsyncBusHandle handle, IRequestClient<SubmitOrder> client)
        {
            _handle = handle;
            _client = client;
        }

        [FunctionName("order")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = null)] HttpRequest request, ILogger logger)
        {
            var body = await request.ReadAsStringAsync();

            var order = JsonSerializer.Deserialize<SubmitOrder>(body, SystemTextJsonMessageSerializer.Options);
            if (order == null)
                return new BadRequestResult();

            logger.LogInformation("SubmitOrder HTTP Function: {OrderId} -> {OrderNumber}", order.OrderId, order.OrderNumber);

            var response = await _client.GetResponse<OrderAccepted>(order);

            return new OkObjectResult(new
            {
                response.Message.OrderId
            });
        }
    }
}