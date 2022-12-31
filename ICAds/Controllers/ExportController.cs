using Microsoft.AspNetCore.Mvc;
using ICSharpCode.SharpZipLib.Zip;
using ICAds.Content.Image;
using ICAds.Content.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ICAds.Controllers
{
    [ApiController]
    [Route("export")]
    public class ExportController : TokenController
    {

        //private Microsoft.AspNetCore.Hosting.IHostingEnvironment _oIHostingEnv;
        
        [HttpPost]            
        public async Task<string> GenerateZip([FromBody] GenerateMultipleTemplateDTO request)
        {
            //var webRoot = _oIHostingEnv.WebRootPath;
            //var fileName = "Export.zip";
            ////var tempOutput = webRoot + "/static/" + fileName;
            //var tempOutput = $"./../images/{fileName}";
            //using (ZipOutputStream zipOutputStream = new ZipOutputStream(System.IO.File.Create(tempOutput)))
            //{
            //    zipOutputStream.SetLevel(9);

            //    byte[] buffer = new byte[4096];

            //    var imageList = new List<byte[]>();


            //    foreach (GenerateTemplateDTO instance in generationList)
            //    {
            //        var imageData = await ImageProcessor.GenerateFromTemplate2(instance.Template, instance.Variables);

            //        byte[] imageArray = imageData.ToArray();

            //        imageList.Add(imageArray);
            //    }


            //    int i = 0;
            //    foreach (byte[] imageBytes in imageList)
            //    {
            //        ZipEntry entry = new ZipEntry(i.ToString());
            //        entry.DateTime = DateTime.Now;
            //        entry.IsUnicodeText = true;
            //        zipOutputStream.PutNextEntry(entry);
            //        zipOutputStream.Write(imageBytes);
            //        i++;
            //    }

            //    zipOutputStream.Finish();
            //    zipOutputStream.Flush();
            //    zipOutputStream.Close();

            //}

            return "done";
        }

    }
}

