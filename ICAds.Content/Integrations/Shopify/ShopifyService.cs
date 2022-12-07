using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Newtonsoft.Json;
using ICAds.Data.Models;
using Newtonsoft.Json.Linq;
using System.Xml.Linq;
using ICAds.Content.Models;

namespace ICAds.Content.Integrations.Shopify
{
    public class ShopifyService
    {

        //readonly ShopifySettings settings = new ShopifySettings("https://ictesting.myshopify.com/", "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");
        //readonly ShopifySettings settings = new ShopifySettings("https://butler-loftet.dk/products.json", "");
        readonly ShopifySettings settings = new ShopifySettings();
        readonly ApiHelper httpClient = new ApiHelper();
        

        //readonly ShopifySettings settings;

        //public ShopifyService(ShopifySettings config)
        //{
        //    settings = config;
        //}

        public ShopifyService(IntegrationModel integration)
        {
            settings.AccessToken = integration.AccessToken;
            settings.Url = integration.Url;

            httpClient.SetDefaultHeader("X-Shopify-Access-Token", settings.AccessToken);
        }


        public async Task<SingleProduct> GetSingleProduct(string productId)
        {
            var result = await httpClient.GetAsync(getRestUrl(settings.Url, $"products/{productId}.json"));
            var json = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<SingleProduct>(json);
        }

        public async Task<GraphProductResponse> SearchProducts(string searchTerm)
        {
            // Query params/terms can be linked like below
            // "has_only_default_variant:true:water"
            var result = await httpClient.PostAsync(getGraphUrl(settings.Url), new StringContent("{products(first: 10, query: \"" + searchTerm + "\"){edges{node{id, title}}}}", Encoding.UTF8, "application/graphql"));
            var json = result.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<GraphProductResponse>(json);
        }


        public async Task<List<Variable>> GetProductVariables(string productId)
        {

            var product = await GetSingleProduct(productId);
            List<Variable> variables = new List<Variable>();

            var variantCount = 1;
            foreach (var variant in product.Product.Variants)
            {
                foreach(var varProp in variant.GetType().GetProperties())
                {
                    var name = $"variant{variantCount}_";
                    variables.Add(new Variable($"{name}{varProp.Name}", varProp.GetValue(variant)?.ToString() ?? ""));
                }
                variantCount++;
            }

            var imgCount = 1;
            foreach (var image in product.Product.Images)
            {
                foreach (var imgProp in image.GetType().GetProperties())
                {
                    var name = $"image{imgCount}_";
                    variables.Add(new Variable($"{name}{imgProp.Name}", imgProp.GetValue(image)?.ToString() ?? ""));
                }
                imgCount++;
            }

            foreach (var prop in product.Product.GetType().GetProperties())
            {
                if(prop.Name != "Images" && prop.Name != "Variants" && prop.GetValue(product.Product) != null)
                {
                    variables.Add(new Variable(prop.Name, prop.GetValue(product.Product)?.ToString() ?? ""));
                }
            }

            return variables;

        }



        //public static async Task<ShopifyProductListResult> GetContent(string url)
        //{
        //    var result = await new ApiHelper().GetAsync();
        //    var json = result.Content.ReadAsStringAsync().Result;

        //    return JsonConvert.DeserializeObject<ShopifyProductListResult>(json);
        //}


        public async Task<ShopifyProductListResult> GetProducts()
        {
            string url = settings.Url;
            var result = await new ApiHelper().GetAsync(url);
            var json = result.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<ShopifyProductListResult>(json);
        }


        public async Task<IEnumerable<ShopifyProduct>> SearchDemoProduct(string searchTerm)
        {
            var products = (await GetProducts()).Products;

            IEnumerable<ShopifyProduct> result = products.Where(p => p.Title.Contains(searchTerm))
                .Select(p => new ShopifyProduct
                {
                    Id = p.Id,
                    Title = p.Title,
                    Vendor = p.Vendor,
                    //Tags = p.Tags,
                    Variants = p.Variants,
                    Images = p.Images,
                    Price = p.Price
                });

            //    IEnumerable < ShopifyProduct > result = products.Where(p => p.Title.Contains(searchTerm))
            //.Select(p => new ShopifyProduct(p));
            return result;

        }

        public Uri getGraphUrl(string url)
        {
            string scheme = url.StartsWith("https://") ? "" : "https://";
            string apiPath = url.EndsWith("/") ? "admin/api/2022-10/graphql.json" : "/admin/api/2022-10/graphql.json";
            string graphUrl = $"{scheme}{url}{apiPath}";

            return new Uri(graphUrl);
        }

        

        public string getRestUrl(string url, string path)
        {
            string scheme = url.StartsWith("https://") ? "" : "https://";
            string apiPath = url.EndsWith("/") ? "admin/api/2022-10" : "/admin/api/2022-10";
            string restUrl = $"{scheme}{url}{apiPath}/{path}";

            return new string(restUrl);
        }
    }


    public class ShopifySettings
    {
        public ShopifySettings()
        {
            Url = "";
            AccessToken = "";
        }

        public ShopifySettings(string url, string accessToken)
        {
            Url = url;
            AccessToken = accessToken;

        }

        public string Url { get; set; }
        public string AccessToken { get; set; }

    }
}
