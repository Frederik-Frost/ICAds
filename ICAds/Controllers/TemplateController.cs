using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICAds.Data.Models;
using ICAds.Data.DTO;
using ICAds.Data.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using ICAds.Content.Integrations.Shopify;
using ICAds.Content.Models;
using System.Text.Json;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICAds.Controllers
{
    [Route("templates")]
    [Authorize]
    public class TemplateController : TokenController
    {
        [Route("metadata")]
        [HttpGet]
        public async Task<ActionResult<List<TemplateMetadataModel>>> GetTemplates()
        {
            var templates = await TemplateRepository.GetTemplates(GetOrgId());
            return Ok(templates);
        }

        [HttpPut]
        [Route("{templateId}/metadata")]
        public async Task<ActionResult<TemplateMetadataModel>> UpdateTemplateMetadata([FromBody] TemplateDTO templateInfo, string templateId)
        {
            var template = await TemplateRepository.UpdateTemplateMetadata(GetOrgId(), templateId, templateInfo);
            return Ok(template);

        }

        [HttpPost]
        public async Task<ActionResult<TemplateMetadataModel>> CreateTemplate([FromBody] TemplateDTO templateInfo)
        {
            var template = await TemplateRepository.CreateTemplate(GetOrgId(), GetUserId(), templateInfo);
            return Ok(template);
        }

        [HttpDelete]
        [Route("{templateId}")]
        public async Task<ActionResult> DeleteTemplate(string templateId)
        {
            var template = await TemplateRepository.DeleteTemplate(GetOrgId(), templateId);
            if (template == false) return NotFound("No layout with this id found");
            else return Ok("Deleted layout");
        }

        [HttpGet]
        [Route("{templateId}")]
        public async Task<ActionResult<TemplateMetadataModel>> GetTemplate(string templateId)
        {
            var template = await TemplateRepository.GetTemplate(GetOrgId(), templateId);
            if (template == null) return NotFound("No layout with this id found");
            else return Ok(template);
        }

        [HttpPut]
        [Route("{templateId}/save")]
        public async Task<ActionResult<TemplateMetadataModel>> SaveTemplateJson([FromBody] JsonDocument templateJson, string templateId)
        {
            var template = await TemplateRepository.SaveTemplateJson(GetOrgId(), GetUserId(), templateId, templateJson);
            if (template == null) return NotFound("No layout with this id found");
            return Ok(template);
        }

        [HttpGet]
        [Route("{templateId}/products/search")]
        public async Task<ActionResult<GraphProductResponse>> SearchTemplateProduct(string templateId, string query)
        {
            var template = await TemplateRepository.GetTemplate(GetOrgId(), templateId);
            if (template == null) return NotFound("Could not find integration");

            return await new ShopifyService(template.Integration).SearchProducts(query);    
        }

        [HttpGet]
        [Route("{templateId}/products/{productId}")]
        public async Task<ActionResult<SingleProduct>> GetProductFromId(string templateId, string productId)
        {
            var template = await TemplateRepository.GetTemplate(GetOrgId(), templateId);
            if (template == null) return NotFound("Could not find integration");

            return await new ShopifyService(template.Integration).GetSingleProduct(productId);
        }

        [HttpGet]
        [Route("{templateId}/products/{productId}/variables")]
        public async Task<ActionResult<List<Variable>>> GetProductVariablesFromId(string templateId, string productId)
        //public async Task<ActionResult<string>> GetProductVariablesFromId(string templateId, string productId)
        {
            var template = await TemplateRepository.GetTemplate(GetOrgId(), templateId);
            if (template == null) return NotFound("Could not find integration");
            
            return await new ShopifyService(template.Integration).GetProductVariables(productId);
        }

    }
}

