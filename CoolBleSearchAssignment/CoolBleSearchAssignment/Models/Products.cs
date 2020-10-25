using Newtonsoft.Json;
using System.Collections.Generic;

namespace CoolBleSearchAssignment.Models
{

    public class ProductList
    {
        [JsonProperty("products")]
        public List<Product> Products { get; set; }
        [JsonProperty("currentPage")]
        public int currentPage { get; set; }
        [JsonProperty("pageSize")]
        public int pageSize { get; set; }
        [JsonProperty("totalResults")]
        public int totalResults { get; set; }
        [JsonProperty("pageCount")]
        public int pageCount { get; set; }
    }

    public class Product
    {
        [JsonProperty("productId")]
        public int ProductID { get; set; }

        [JsonProperty("productName")]
        public string ProductName { get; set; }
        [JsonProperty("reviewInformation")]
        public ReviewInfomation ReviewInformation { get; set; }
        [JsonProperty("USPs")]
        public List<string> USPs { get; set; }
        [JsonProperty("availabilityState")]
        public int AvailabilityState { get; set; }
        [JsonProperty("salesPriceIncVat")]
        public double SalesPriceIncVat { get; set; }

        [JsonProperty("listPriceIncVat")]
        public double ListPriceIncVat { get; set; } = 0;
        [JsonProperty("productImage")]
        public string ProductImage { get; set; }
        [JsonProperty("coolbluesChoiceInformationTitle")]
        public string CoolBluesChoiceInformationTitle { get; set; }
        [JsonProperty("promoIcon")]
        public PromoIcon PromoIcon { get; set; }
        [JsonProperty("nextDayDelivery")]
        public bool NextDayDelivery { get; set; }


    }

    public class ReviewInfomation
    {
        [JsonProperty("reviews")]
        public List<object> Reviews { get; set; }
        [JsonProperty("reviewSummary")]
        public ReviewSummary ReviewSummary { get; set; }
    }

    public class PromoIcon
    {
        [JsonProperty("text")]
        public string Text { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
    }

    public class ReviewSummary
    {
        [JsonProperty("reviewAverage")]
        public string ReviewAverage { get; set; }
        [JsonProperty("reviewCount")]
        public string ReviewCount { get; set; }
    }
}
