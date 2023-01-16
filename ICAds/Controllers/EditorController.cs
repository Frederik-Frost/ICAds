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
using Newtonsoft.Json;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.Checksum;

namespace ICAds.Controllers
{
    [Authorize]
    [Route("editor")]
    public class EditorController : TokenController
    {

        [Route("generate")]
        [HttpPost]
        public async Task<byte[]> GenerateFromTemplate([FromBody] GenerateTemplateDTO generationData)
        {

            var imageData = await ImageProcessor.GenerateFromTemplate(generationData.Template, generationData.Variables);

            byte[] imageArray = imageData.ToArray();
            //byte[] imageArray = new byte[2];

            //MemoryStream memoryStream = new MemoryStream(imageArray);
            //HttpResponseMessage res = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            //res.Content = new StreamContent(memoryStream);
            //res.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");


            return imageArray;
        }

        [Route("export")]
        [HttpPost]
        public async Task<byte[]> GenerateZip([FromBody] List<GenerateTemplateDTO> request)
        {
            Crc32 crc = new Crc32();
            using (MemoryStream zipMemoryStream = new MemoryStream())
            {
                ZipOutputStream zipOutputStream = new ZipOutputStream(zipMemoryStream);
                zipOutputStream.SetLevel(9);

                int i = 0;
                var imageList = new List<byte[]>();
                foreach (GenerateTemplateDTO instance in request)
                {
                    var imageData = await ImageProcessor.GenerateFromTemplate(instance.Template, instance.Variables);
                    byte[] imageArray = imageData.ToArray();
                    
                    ZipEntry entry = new ZipEntry($"img_{i.ToString()}.png");
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    crc.Reset();
                    crc.Update(imageArray);
                    entry.Crc = crc.Value;
                    zipOutputStream.PutNextEntry(entry);
                    zipOutputStream.Write(imageArray, 0, imageArray.Length);

                    i++;
                }

                zipOutputStream.Finish();
                byte[] zipByteArray = new byte[zipMemoryStream.Length];
                zipMemoryStream.Position = 0;
                zipMemoryStream.Read(zipByteArray, 0, (int)zipMemoryStream.Length);
                zipOutputStream.Flush();
                zipOutputStream.Close();

                return zipByteArray;
            }
        }
    }
}

