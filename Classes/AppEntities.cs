using System;
using System.Text.Json.Serialization;

namespace GoldAppClassLibrary
{
    public class ApiResponce
    {
        public DateTimeOffset TimeStamp { get; set; }

        public string MetalCode { get; set; }
        public string Currency { get; set; }
        public decimal CurrentPrice { get; set; }
        public decimal AverigePriceOver100Days { get; set; }
        public string Symbol { get; set; }
    }

    public class GoldData
    {
        public string PartitionKey { get; set; }
        public string RowKey { get; set; }
        public string FromSource { get; set; }
        public DateTimeOffset TimeStamp { get; set; }
        public string Metal { get; set; }
        public string Currency { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
    }
    public class ResponceObj
    {
        public int TimeStamp { get; set; }
        public string Metal { get; set; }
        public string Currency { get; set; }
        public string Exchange { get; set; }
        public string Symbol { get; set; }
        public double Prev_Close_Price { get; set; }
        public double Open_Price { get; set; }
        public double Low_Price { get; set; }
        public double High_Price { get; set; }
        public int Open_Time { get; set; }
        public decimal Price { get; set; }
        [JsonIgnore]
        public double Ch { get; set; }
        [JsonIgnore]
        public double Chp { get; set; }
        [JsonIgnore]
        public double Ask { get; set; }
        [JsonIgnore]
        public double Bid { get; set; }
    }
}