using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ICAds.Data.DTO;
using ICAds.Data.Repositories;
using ICAds.Content.Image;
using ICAds.Content.Models;
using ICAds.Content.Integrations.Shopify;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkiaSharp;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICAds.Controllers
{
    //[Authorize]
    [Route("editor")]
    public class EditorController : TokenController
    {
        
        [HttpPost]
        public async Task<Stream> GenerateFromTemplate([FromBody] GenerateTemplateDTO generationData)
        {

            var res = await ImageProcessor.GenerateFromTemplate(generationData.Template, generationData.ProductData);
            //return res.ToArray();
            return res.AsStream();

        }

    }

    
}

