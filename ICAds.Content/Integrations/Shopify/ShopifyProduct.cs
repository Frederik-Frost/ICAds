using System;
using System.Numerics;
using System.Runtime.Intrinsics.Arm;
using Newtonsoft.Json;
using static System.Net.Mime.MediaTypeNames;

namespace ICAds.Content.Integrations.Shopify
{
    public class ShopifyProduct
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Vendor { get; set; }
        public List<string> Tags { get; set; }
        public List<ProductVariant> Variants { get; set; }
        public List<ProductImage> Images { get; set; }
        public string Price { get; set; }
    }

    //public class ShopifyProduct(string p)
    //{
    //    Id = p.Id 
    //    Title = p.Title 
    //    Vendor = p.Vendor
    //    Tags = p.Tags
    //    Variants =  p.Variants
    //    Images = p.Images
    //    Price = p.Price 
    //}

    public class ProductVariant
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public string Price { get; set; }
        [JsonProperty("product_id")]
        public long ProductId { get; set; }
        [JsonProperty("compare_at_price")]
        public string CompareAtPrice { get; set; }
    }

    public class ProductImage
    {
        public long Id { get; set; }
        public int Position { get; set; }
        [JsonProperty("product_id")]
        public long ProductId { get; set; }
        public string Src { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }


    public class ShopifyProductListResult
    {
        public List<ShopifyProduct> Products { get; set; }
    }

}

