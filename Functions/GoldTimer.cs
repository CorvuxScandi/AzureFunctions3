using System;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using GoldAppClassLibrary;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace Corvux.Function
{
    public static class GoldTimer
    {
        [Function("GetGoldTimer")]
        [QueueOutput("goldqueue", Connection = "AzureWebJobsStorage")]
        public static async Task<ResponceObj> Run([TimerTrigger("* 0 * * * *")] FunctionContext context)
        {
            HttpClient client = new HttpClient();

            var logger = context.GetLogger("GetGoldTimer");
            logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");

            client.DefaultRequestHeaders.Add("x-access-token", "goldapi-4c8svtku3tzeh3-io");
            var responce = await client.GetAsync(new Uri("https://www.goldapi.io/api/XAU/EUR"));
            if (responce.IsSuccessStatusCode)
            {
                var jsonString = await responce.Content.ReadAsStringAsync();
                var resObj = JsonSerializer.Deserialize<ResponceObj>(jsonString,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return resObj;
            }
            else
            {
                logger.LogInformation("Request error");
                logger.LogError(responce.RequestMessage.ToString());
                return null;
            }
        }


    }
}