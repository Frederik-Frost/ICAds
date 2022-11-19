using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;

namespace ICAds.Content.Integrations.Shopify
{
    public class ShopifyService
    {

        //readonly ShopifySettings settings = new ShopifySettings("https://ictesting.myshopify.com/", "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");
        readonly ShopifySettings settings = new ShopifySettings("https://butler-loftet.dk/products.json", "");
        readonly ApiHelper httpClient = new ApiHelper();
        //readonly ShopifySettings settings;

        //public ShopifyService(ShopifySettings config)
        //{
        //    settings = config;
        //}

        public static async Task<ShopifyProductListResult> GetContent(string url)
        {
            var result = await new ApiHelper().GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<ShopifyProductListResult>(json);
        }


        public async Task<ShopifyProductListResult> GetProducts()
        {
            string url = settings.URL;
            var result = await new ApiHelper().GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<ShopifyProductListResult>(json);
        }


        public async Task<IEnumerable<ShopifyProduct>> SearchProduct(string searchTerm)
        {
            var products = (await GetProducts()).Products;

            IEnumerable<ShopifyProduct> result = products.Where(p => p.Title.Contains(searchTerm))
                .Select(p => new ShopifyProduct
                {
                    Id = p.Id,
                    Title = p.Title,
                    Vendor = p.Vendor,
                    Tags = p.Tags,
                    Variants = p.Variants,
                    Images = p.Images,
                    Price = p.Price
                });

            //    IEnumerable < ShopifyProduct > result = products.Where(p => p.Title.Contains(searchTerm))
            //.Select(p => new ShopifyProduct(p));
            return result;

        }




    }

    public class ShopifySettings
    {
        public ShopifySettings()
        {
            URL = "";
            AccessToken = "";
        }

        public ShopifySettings(string url, string accessToken)
        {
            URL = url;
            AccessToken = accessToken;

        }

        public string URL { get; set; }
        public string AccessToken { get; set; }

    }
}
