using System;
using GoldAppClassLibrary;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Corvux.Function
{
    public static class QueueToStorage
    {
        [Function("QueueToStorage")]
        [TableOutput("golddata", Connection = "AzureWebJobsStorage")]
        public static GoldData Run([QueueTrigger("goldqueue", Connection = "AzureWebJobsStorage")] ResponceObj obj, FunctionContext context)
        {
            var logger = context.GetLogger("Messages");
            logger.LogInformation("Queuetrigger function");

            return new GoldData
            {
                PartitionKey = "GoldApi.io",
                RowKey = $"{(DateTimeOffset.MaxValue.Ticks - obj.TimeStamp):d10}-{Guid.NewGuid():N}",
                Currency = obj.Currency,
                Metal = obj.Metal,
                Price = obj.Price,
                Symbol = obj.Symbol,
                TimeStamp = DateTimeOffset.FromUnixTimeSeconds(obj.TimeStamp),
                FromSource = "GoldApi.io"
            };
        }
    }
}