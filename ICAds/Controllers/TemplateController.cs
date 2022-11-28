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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICAds.Controllers
{
    [Route("templates")]
    [Authorize]
    public class TemplateController : TokenController
    {
        [HttpGet]
        public async Task<ActionResult<List<TemplateMetadataModel>>> GetTemplates()
        {
            var templates = await TemplateRepository.GetTemplates(GetOrgId());
            return Ok(templates);
        }


        [HttpPost]
        public async Task<ActionResult<TemplateMetadataModel>> CreateTemplate([FromBody] TemplateDTO templateInfo)
        {
            var template = await TemplateRepository.CreateTemplate(GetOrgId(), GetUserId(), templateInfo);
            return Ok(template);
        }

        [HttpPut]
        [Route("{templateId}")]
        public async Task<ActionResult<TemplateMetadataModel>> UpdateTemplateMetadata([FromBody] TemplateDTO templateInfo , string templateId)
        {
            var template = await TemplateRepository.UpdateTemplateMetadata(GetOrgId(), templateId, templateInfo);
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

    }
}

