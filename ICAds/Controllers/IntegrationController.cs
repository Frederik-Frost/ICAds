

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

}



