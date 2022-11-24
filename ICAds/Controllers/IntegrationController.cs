

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ICAds.Content.Integrations.Shopify;
using ICAds.Data.Models;
using ICAds.Data.DTO;
using ICAds.Data.Repositories;

namespace ICAds.Controllers
{
    [Route("/integrations")]
    [Authorize]
    public class IntegrationController : TokenController
    {

        [Route("/shopify")]
        [HttpPost]
        public async Task<ActionResult<IntegrationModel>> CreateIntegration([FromBody]IntegrationDTO request)
        {
            var newIntegration = await IntegrationRepository.CreateIntegration(GetOrgId(), request);
            return newIntegration;
        }


        [Route("products/{productId}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<SingleProduct> GetSingleProduct(string productId)
        {

            //var integration = await IntegrationRepository.GetIntegrationById(request.id);

            var integration = new IntegrationModel();
            integration.AccessToken = "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";
            integration.Id = "695f2d11-7429-4d22-a736-cb461a1deb04";
            integration.OrganizationId = "455ab2b8-6655-4db1-9aaa-5b9f6c4e12eb";
            integration.Name = "dev integration";
            integration.Url = "ictesting.myshopify.com";

            return await new ShopifyService(integration).GetSingleProduct(productId);
        }

        [Route("products/search")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<GraphProductResponse> SearchProducts([FromBody] Testbody request)
        {

            //var integration = await IntegrationRepository.GetIntegrationById(request.id);

            var integration = new IntegrationModel();
            integration.AccessToken = "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";
            integration.Id = "695f2d11-7429-4d22-a736-cb461a1deb04";
            integration.OrganizationId = "455ab2b8-6655-4db1-9aaa-5b9f6c4e12eb";
            integration.Name = "dev integration";
            integration.Url = "ictesting.myshopify.com";

            return await new ShopifyService(integration).SearchProducts(request.searchTerm);
        }

        [Route("getallintegration")]
        [HttpGet]
        public async Task<ActionResult<List<IntegrationModel>>> GetOrganizationIntegrations()
        {
            var integrations = await IntegrationRepository.GetIntegrations(GetOrgId());
            //var integration = await IntegrationRepository.GetIntegrationById(integrationId);
            //if (integration == null) return NotFound("No Integration with this id found");
            return Ok(integrations);
        }

        [Route("getintegration")]
        [HttpPost]
        public async Task<ActionResult<IntegrationModel>> GetIntegrationById([FromBody] string integrationId)
        {
            var integration = await IntegrationRepository.GetIntegrationById(integrationId);
            if (integration == null) return NotFound("No Integration with this id found");
            else return Ok(integration);
        }


        // GET: api/values
        //[HttpGet]
        //public Task<ShopifyProductListResult> Get()
        //{
        //    return new ShopifyService().GetProducts();
        //}

        //[Route("~/content")]
        //[HttpPost]
        //[Consumes("application/json")]
        //public Task<ShopifyProductListResult> Post([FromBody] string url)
        //{
        //    return ShopifyService.GetContent(url);
        //}

        //[Route("~/search")]
        //[HttpPost]
        //[Consumes("application/json")]
        //public Task<IEnumerable<ShopifyProduct>> Search([FromBody] string searchTerm)
        //{
        //    return new ShopifyService().SearchProduct(searchTerm);
        //}

    }

    public class Testbody
    {
        public string searchTerm { get; set; }
        public string id { get; set; }
    }

}



