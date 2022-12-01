using System;
using System.Drawing;
using System.Security.Cryptography;
using ICAds.Content.Integrations.Shopify;
using ICAds.Content.Models;
using SkiaSharp;
using static System.Net.Mime.MediaTypeNames;

namespace ICAds.Content.Image
{
	public class ImageProcessor
	{
		public static async Task<SKData> GenerateFromTemplate(TemplateStructure template, ShopifyProduct productData)
		{
            // Create the base from the width and height
            var imgData = productData.Images[0];
            bool productImageIsLandscape = imgData.Width >= imgData.Height;
            
            SKBitmap imageBg = await GetImageFromUrl(imgData.Src);
            SKBitmap scaledImageBg = scaleImageBg(imageBg, imgData, template.Width, template.Height, productImageIsLandscape);

            SKSurface surface = SKSurface.Create(new SKImageInfo(template.Width, template.Height));
            SKCanvas canvas = surface.Canvas;

            // TO center image
            float imgX = 0;
            float imgY = 0;
            if (productImageIsLandscape)
            {
                imgX = -Math.Abs(((float)scaledImageBg.Width - (float)template.Width) / (float)2);
                //float imgX = ((float)scaledImageBg.Width - (float)template.Width) / (float)2;
                //float NegX = -Math.Abs(imgX);
            } else
            {
                imgY = -Math.Abs(((float)scaledImageBg.Height - (float)template.Height) / (float)2);
            }

            // Actually drawing the image to the canvas
            canvas.DrawBitmap(scaledImageBg, imgX, imgY);



            foreach (TextLayer l in template.Layers)
            {
                if(l.LayerType == "TextLayer")
                {
                   canvas = ApplyTextLayerToCanvas(canvas, l);
                }
                System.Diagnostics.Debug.WriteLine(l.LayerType);
                System.Diagnostics.Debug.WriteLine(l);
            }


            // Print actual image
            SKImage image = surface.Snapshot();
            SKBitmap bitmap = SKBitmap.FromImage(image);
            SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100);

            // 
            //SaveImage(data, "generated.png");

            return data;
        }
            
        public static SKCanvas ApplyTextLayerToCanvas(SKCanvas canvas, TextLayer layer)
        {
            SKPaint paint = new SKPaint();
            paint.Color = SKColor.Parse(layer.TextColor);
            paint.TextSize = layer.TextSize;
            paint.Typeface = SKTypeface.FromFamilyName(layer.FontFamily);
            paint.IsAntialias = true;
            // Find the text bounds
            SKRect textBounds = new SKRect();
            paint.MeasureText(layer.Text, ref textBounds);

            // makes sure that if small letters go below baseline, then the center is not calculated with that.
            float yIgnorBelowBaseline = layer.PosY - (textBounds.MidY / 2);

            if (layer.HasBackground == true)
            {
                SKRect frameRect = textBounds;
                frameRect.Offset(layer.PosX, layer.PosY);
                frameRect.Inflate(layer.InflateX, layer.InflateY);

                SKPaint framePaint = new SKPaint
                {
                    Style = layer.BackgroundStyle == "Fill" ? SKPaintStyle.Fill : SKPaintStyle.Stroke,
                    Color = SKColor.Parse(layer.BackgroundColor)
                };
                
                canvas.DrawRoundRect(frameRect, layer.BorderRadius, layer.BorderRadius, framePaint);
            }

            canvas.DrawText(layer.Text, layer.PosX, layer.PosY, paint);
            //canvas.DrawText(layer.Text, layer.PosX, yIgnorBelowBaseline, paint);
            return canvas;
        }



        public static async Task<SKBitmap> GetImageFromUrl(string url)
        {
            HttpClient httpClient = new HttpClient();
            SKBitmap webBitmap;
            using (Stream stream = await httpClient.GetStreamAsync(url))
            using (MemoryStream memStream = new MemoryStream())
            {
                await stream.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);
                webBitmap = SKBitmap.Decode(memStream);
                return webBitmap;
            }

        }


        public static SKBitmap scaleImageBg(SKBitmap bitmap, ProductImage imageData, int templateWidth, int templateHeight, bool productImageIsLandscape )
        {
            // factor = desired width / actual width or desired height / actual height
            double resizeFactor = productImageIsLandscape ? (double)((decimal)templateHeight / (decimal)imageData.Height) : (double)((decimal)templateWidth / (decimal)imageData.Width);

            int width = (int)Math.Round(bitmap.Width * resizeFactor);
            int height = (int)Math.Round(bitmap.Height * resizeFactor);

            SKBitmap scaledBitmap = new SKBitmap(width, height, bitmap.ColorType, bitmap.AlphaType);
            SKCanvas canvas = new SKCanvas(scaledBitmap);

            canvas.SetMatrix(SKMatrix.CreateScale((float)resizeFactor, (float)resizeFactor));
            canvas.DrawBitmap(bitmap, new SKPoint());
            canvas.ResetMatrix();
            canvas.Flush();

            return scaledBitmap;
        }


        public static void SaveImage(SKData data, string filename)
        {
            using (FileStream file = File.Create($"./../images/{filename}"))
            {
                data.AsStream().Seek(0, SeekOrigin.Begin);
                data.AsStream().CopyTo(file);

                file.Flush();
                file.Close();
            }
        }

    }
}

