using System;
namespace ICAds.Content.Integrations.Shopify
{
    public class ShopifyUrlModel
    {
        public ShopifyUrlModel(string url)
        {
            Url = url;

        }

        public ShopifyUrlModel()
        {

        }

        public string Url { get; set; }

    }
}

