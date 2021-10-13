
using System;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using GoldAppClassLibrary;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Corvux.Function
{
    public static class MyGoldApi
    {
        [Function("MyGoldApi")]
        public static async Task<HttpResponseData> Run([HttpTrigger(AuthorizationLevel.Anonymous, "get")] HttpRequestData req,
        [TableInput("golddata", "GoldApi.io", Take = 100)] GoldData[] TabelData,
            FunctionContext executionContext)
        {
            var logger = executionContext.GetLogger("MyGoldApi");
            logger.LogInformation("C# HTTP trigger function processed a request.");
            var bodyContent = req.Url.ToString().Split('=').Last().ToUpper();
            if (bodyContent == "") bodyContent = "EUR";
            if (TabelData.Any())
            {
                var latest = TabelData.First();
                ApiResponce apiResponse = new()
                {
                    AverigePriceOver100Days = TabelData.Average(x => x.Price),
                    Currency = bodyContent,
                    CurrentPrice = latest.Price,
                    MetalCode = latest.Metal,
                    Symbol = latest.Symbol,
                    TimeStamp = latest.TimeStamp
                };

                if (bodyContent != "" && bodyContent.ToUpper() != "EUR")
                {
                    HttpClient client = new();
                    var result = await client.GetAsync($"https://api.frankfurter.app/latest?to={bodyContent}");
                    if (!result.IsSuccessStatusCode)
                    {
                        return req.CreateResponse(HttpStatusCode.BadRequest);
                    }
                    if (result.IsSuccessStatusCode)
                    {
                        var jsonString = await result.Content.ReadAsStringAsync();
                        var rate = jsonString.Split(':').Last().Remove(7, 2);
                        var convertedRate = decimal.Parse(rate, CultureInfo.InvariantCulture);

                        apiResponse.AverigePriceOver100Days *= convertedRate;
                        apiResponse.CurrentPrice *= convertedRate;
                    }

                }


                var responce = req.CreateResponse(HttpStatusCode.OK);
                await responce.WriteStringAsync(JsonConvert.SerializeObject(apiResponse));
                return responce;
            }
            return req.CreateResponse(HttpStatusCode.NotFound);
        }
    }
}
