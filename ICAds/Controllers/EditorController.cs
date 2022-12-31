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

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICAds.Controllers
{
    [Authorize]
    [Route("editor")]
    public class EditorController : TokenController
    {

        //[HttpPost]
        //public async Task<Stream> GenerateFromTemplate([FromBody] GenerateTemplateDTO generationData)
        //{

        //var res = await ImageProcessor.GenerateFromTemplate(generationData.Template, generationData.ProductData);
        //    //return res.ToArray();
        //    return res.AsStream();

        //}

        [HttpGet]
        public string TestLoops()
        {

            //Layer.ImageLayer first = new ImageLayer();
            //Layer.TextLayer sec = new TextLayer();
            //Layer.ShapeLayer third = new ShapeLayer();
            //Layer.TextLayer fourth = new TextLayer();
            //first.LayerType = "ImageLayer";
            //sec.LayerType = "TextLayer";
            //third.LayerType = "ShapeLayer";
            //TemplateStructure dummy = new TemplateStructure();
            //dummy.Layers = new List<Layer>();
            //dummy.Layers.Add(first);
            //dummy.Layers.Add(sec);
            //dummy.Layers.Add(third);
            //dummy.Layers.Add(fourth);


            //ImageProcessor.TestLoop(dummy);

            //var res = await ImageProcessor.GenerateFromTemplate(generationData.Template, generationData.ProductData);
            //return res.ToArray();
            return "hello";

        }

        [Route("generate")]
        [HttpPost]
        public async Task<byte[]> GenerateFromTemplate2([FromBody] GenerateTemplateDTO generationData)
        {

            var imageData = await ImageProcessor.GenerateFromTemplate2(generationData.Template, generationData.Variables);

            byte[] imageArray = imageData.ToArray();
            //byte[] imageArray = new byte[2];

            //MemoryStream memoryStream = new MemoryStream(imageArray);
            //HttpResponseMessage res = new HttpResponseMessage(System.Net.HttpStatusCode.OK);

            //res.Content = new StreamContent(memoryStream);
            //res.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");


            return imageArray;

            //using (var memoryStream = new MemoryStream())
            //{

            //}


        }


        //private Microsoft.AspNetCore.Hosting.IHostingEnvironment IHostingEnv;

        [Route("export")]
        [HttpPost]
        public async Task<byte[]> GenerateZip([FromBody] List<GenerateTemplateDTO> request)
        {
            Crc32 crc = new Crc32();

            using (MemoryStream zipMemoryStream = new MemoryStream())
            {
                ZipOutputStream zipOutputStream = new ZipOutputStream(zipMemoryStream);

                zipOutputStream.SetLevel(9);

                var imageList = new List<byte[]>();
                foreach (GenerateTemplateDTO instance in request)
                {
                    var imageData = await ImageProcessor.GenerateFromTemplate2(instance.Template, instance.Variables);

                    byte[] imageArray = imageData.ToArray();

                    imageList.Add(imageArray);
                }


                int i = 0;
                foreach (byte[] imageBytes in imageList)
                {
                    ZipEntry entry = new ZipEntry($"img_{i.ToString()}.png");
                    entry.DateTime = DateTime.Now;
                    entry.IsUnicodeText = true;
                    zipOutputStream.PutNextEntry(entry);
                    zipOutputStream.Write(imageBytes);

                    crc.Reset();
                    crc.Update(imageBytes);
                    entry.Crc = crc.Value;
                    zipOutputStream.PutNextEntry(entry);
                    zipOutputStream.Write(imageBytes, 0, imageBytes.Length);


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
        //{
        //    //var webRoot = IHostingEnv.WebRootPath;
        //    var fileName = "Export.zip";
        //    //var tempOutput = webRoot + "/static/" + fileName;
        //    var tempOutput = $"./../images/{GetOrgId()}_{fileName}";

        //    using (System.IO.MemoryStream zipMemoryStream = new System.IO.MemoryStream())
        //    using (ZipOutputStream zipOutputStream = new ZipOutputStream(zipMemoryStream))
        //    {
        //        zipOutputStream.SetLevel(9);

        //        byte[] buffer = new byte[4096];

        //        var imageList = new List<byte[]>();


        //        foreach (GenerateTemplateDTO instance in request)
        //        {
        //            var imageData = await ImageProcessor.GenerateFromTemplate2(instance.Template, instance.Variables);

        //            byte[] imageArray = imageData.ToArray();

        //            imageList.Add(imageArray);
        //        }


        //        int i = 0;
        //        foreach (byte[] imageBytes in imageList)
        //        {
        //            ZipEntry entry = new ZipEntry($"img_{i.ToString()}.png");
        //            entry.DateTime = DateTime.Now;
        //            entry.IsUnicodeText = true;
        //            zipOutputStream.PutNextEntry(entry);
        //            zipOutputStream.Write(imageBytes);
        //            i++;
        //        }

        //        byte[] zipByteArray = new byte[zipOutputStream.Length];
        //        zipOutputStream.Finish();
        //        zipOutputStream.Flush();
        //        zipOutputStream.Close();

        //        return zipByteArray;
        //    }

        //    //byte[] finalResult = System.IO.File.ReadAllBytes(tempOutput);


        //    //return finalResult;
        //    //return File(finalResult, "application/zip", fileName);
        //}
    }


    
}

