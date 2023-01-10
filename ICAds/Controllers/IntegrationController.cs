

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
    [Route("integrations")]
    [Authorize]
    public class IntegrationController : TokenController
    {

        [HttpPost]
        public async Task<ActionResult<IntegrationModel>> CreateIntegration([FromBody]IntegrationDTO request)
        {
            var newIntegration = await IntegrationRepository.CreateIntegration(GetOrgId(), request);
            return newIntegration;
        }

        [HttpGet]
        public async Task<ActionResult<List<IntegrationModel>>> GetOrganizationIntegrations()
        {
            var integrations = await IntegrationRepository.GetIntegrations(GetOrgId());
            //var integration = await IntegrationRepository.GetIntegrationById(integrationId);
            //if (integration == null) return NotFound("No Integration with this id found");
            return Ok(integrations);
        }

        [Route("{integrationId}")]
        [HttpGet]
        public async Task<ActionResult<IntegrationModel>> GetIntegrationById(string integrationId)
        {
            var integration = await IntegrationRepository.GetIntegrationById(integrationId);
            if (integration == null) return NotFound("No Integration with this id found");
            else return Ok(integration);
        }

        [Route("{integrationId}")]
        [HttpPut]
        public async Task<ActionResult<IntegrationModel>> UpdateIntegrationData([FromBody] IntegrationDTO request, string integrationId)
        {
            var integration = await IntegrationRepository.UpdateIntegrationData(request, integrationId);
            if (integration == null) return NotFound("No Integration with this id found");
            else return Ok(integration);
        }

        
        [Route("{integrationId}")]
        [HttpDelete]
        public async Task<ActionResult> DeleteIntegration(string integrationId)
        {
            var integration = await IntegrationRepository.DeleteIntegration(integrationId);
            if (integration == false) return NotFound("No Integration with this id found");
            else return Ok("Deleted integration");
        }




        [Route("{integrationId}/products/{productId}")]
        [HttpGet]
        public async Task<SingleProduct> GetSingleProduct(string productId, string integrationId)
        {


            var integration = await IntegrationRepository.GetIntegrationById(integrationId);
            return await new ShopifyService(integration).GetSingleProduct(productId);
        }

        [Route("{integrationId}/products/search")]
        [HttpGet]
        public async Task<GraphProductResponse> SearchProducts(string integrationId, string query )
        {


;

            var integration = await IntegrationRepository.GetIntegrationById(integrationId);
            return await new ShopifyService(integration).SearchProducts(query);
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



