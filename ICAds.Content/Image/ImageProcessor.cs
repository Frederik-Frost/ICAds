using System;
using System.Linq;
using System.Drawing;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ICAds.Content.Integrations.Shopify;
using ICAds.Content.Models;
using SkiaSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Collections.Generic;

namespace ICAds.Content.Image
{
	public class ImageProcessor
	{

        public static async Task<string> GenerateFromTemplate2(TemplateStructure template, List<Variable> variables)
        {
            // Create canvas with the template width and heigth first
            SKSurface surface = SKSurface.Create(new SKImageInfo(template.Width, template.Height));
            SKCanvas canvas = surface.Canvas;



            // Process each layer in to an element on the canvas
            foreach (Layer layer in template.Layers)
            {
                //ImageLayer il = layer as ImageLayer;
                if (layer.LayerType == "ImageLayer")
                {
                    ImageLayer il = new ImageLayer();
                    il.LayerType = layer.LayerType;
                    il.Source = layer.Source;
                    il.Height = layer.Height;
                    il.Width = layer.Width;
                    il.ObjectFit = layer.ObjectFit;
                    il.AlignHorizontal = layer.AlignHorizontal;
                    il.AlignVertical = layer.AlignVertical;
                    il.PosX = layer.PosX;
                    il.PosY = layer.PosY;

                    // Get variable values from the properties that should be able to take variables
                    string source = ExtractVariableValues(il.Source, variables);
                    System.Diagnostics.Debug.WriteLine("SOURCE IS");
                    System.Diagnostics.Debug.WriteLine(source);

                    // Handle adding Image layers to the canvas here
                    SKBitmap initialImage = await GetImageFromUrl(source);


                    //SKImageInfo imgData = initialImage.Info;
                    // factor = desired width / actual width or desired height / actual height
                    double resizeFactor;
                    // Based on objectfit style Calculate the resizefactor needed for scaling
                    bool productImageIsLandscape = initialImage.Width >= initialImage.Height;
                    //bool productImageIsLandscape = imgData.Width >= imgData.Height;
                    if (il.ObjectFit == "Fit")
                    {
                        resizeFactor = productImageIsLandscape ? (double)((decimal)il.Width / (decimal)initialImage.Width) : (double)((decimal)il.Height / (decimal)initialImage.Height);

                    } else
                    {
                        resizeFactor = productImageIsLandscape ? (double)((decimal)il.Height / (decimal)initialImage.Height) : (double)((decimal)il.Width / (decimal)initialImage.Width);
                    }
                    SKBitmap scaledImage = scaleImageBg(initialImage, resizeFactor);

                    // TO center image
                    //float imgX = 0;
                    //float imgY = 0;
                    float imgX = il.PosX;
                    float imgY = il.PosY;


                    //////// PLEASE MAKE ME PRETTY ////////////
                    if (productImageIsLandscape)
                    {
                        if(il.AlignHorizontal == "Left")
                        {
                            //imgX = 0;
                        }
                        if (il.AlignHorizontal == "Center")
                        {
                            imgX = imgX - Math.Abs(((float)scaledImage.Width - (float)template.Width) / (float)2);
                        }
                        if (il.AlignHorizontal == "Right")
                        {
                            imgX = imgX - Math.Abs(((float)scaledImage.Width - (float)template.Width));
                        }


                        if (il.AlignVertical == "Top")
                        {
                            //imgY = 0;
                        }
                        if (il.AlignVertical == "Center")
                        {
                            imgY = Math.Abs(((float)scaledImage.Height - (float)template.Height) / (float)2);
                        }
                        if (il.AlignVertical == "Bottom")
                        {
                            imgY = Math.Abs(((float)scaledImage.Height - (float)template.Height));
                        }
                    }
                    else
                    {
                        if (il.AlignHorizontal == "Left")
                        {
                            imgX = 0;
                        }
                        if (il.AlignHorizontal == "Center")
                        {
                            imgX = imgX + Math.Abs(((float)scaledImage.Width - (float)template.Width) / (float)2);
                        }
                        if (il.AlignHorizontal == "Right")
                        {
                            imgX = imgX + Math.Abs(((float)scaledImage.Width - (float)template.Width));
                        }



                        if (il.AlignVertical == "Top")
                        {
                            //imgY = 0;
                        }
                        if (il.AlignVertical == "Center")
                        {
                            imgY = imgY - Math.Abs(((float)scaledImage.Height - (float)template.Height) / (float)2);
                        }
                        if (il.AlignVertical == "Bottom")
                        {
                            imgY = imgY - Math.Abs(((float)scaledImage.Height - (float)template.Height));
                        }
                    }
                    //////// PLEASE MAKE ME PRETTY ////////////
                    
                    //imgY = imgY + layer.PosY;
                    //imgX = imgX + layer.PosX;

                    // Actually drawing the image to the canvas
                    canvas.DrawBitmap(scaledImage, imgX, imgY);
                }


                //TextLayer tl = layer as TextLayer;
                if (layer.LayerType == "TextLayer")
                {
                    TextLayer tl = new TextLayer();
                    tl.LayerType = layer.LayerType;
                    tl.Text = ExtractVariableValues(layer.Text, variables);
                    tl.TextSize = layer.TextSize;
                    tl.TextColor = layer.TextColor;
                    tl.FontFamily = layer.FontFamily;
                    tl.PosX = layer.PosX;
                    tl.PosY = layer.PosY;

                    tl.HasBackground = layer.HasBackground;
                    tl.InflateX = layer.InflateX;
                    tl.InflateY = layer.InflateY;
                    tl.BackgroundStyle = layer.BackgroundStyle;
                    tl.BackgroundColor = layer.BackgroundColor;
                    tl.BorderRadius = layer.BorderRadius;

                    ApplyTextLayerToCanvas(canvas, tl);
                    // Handle adding Text layers to the canvas here
                }

                //ShapeLayer sl = layer as ShapeLayer;
                if (layer.LayerType == "ShapeLayer")
                {
                    // Handle adding Shape layers to the canvas here

                    ShapeLayer sl = new ShapeLayer();
                    sl.LayerType = layer.LayerType;
                    sl.Height = layer.Height;
                    sl.Width = layer.Width;
                    sl.BackgroundColor = layer.BackgroundColor;
                    sl.BackgroundStyle = layer.BackgroundStyle;
                    sl.BorderRadius= layer.BorderRadius;
                    sl.PosX = layer.PosX;
                    sl.PosY = layer.PosY;


                    //SKRect shape = new SKRect();

                    SKRect shape = SKRect.Create(sl.PosX, sl.PosY, sl.Width, sl.Height);

                    //SKRect shape = new SKRect();
                    //shape.Offset(sl.PosX, sl.PosY);
                    SKPaint framePaint = new SKPaint
                    {
                        Style = layer.BackgroundStyle == "Fill" ? SKPaintStyle.Fill : SKPaintStyle.Stroke,
                        Color = SKColor.Parse(layer.BackgroundColor)
                    };

                    canvas.DrawRoundRect(shape, sl.BorderRadius, sl.BorderRadius, framePaint);

                }

            }

            // Print actual image
            SKImage image = surface.Snapshot();
            SKBitmap bitmap = SKBitmap.FromImage(image);
            SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100);

            
            SaveImage(data, "generated.png");

            return "DONE";

            
        }

        public static string ExtractVariableValues(string text, List<Variable> variables)
        {

            Regex rx = new Regex(@"\{([^}]*)\}");
            Match match = rx.Match(text);

            if(match.Success == false) {
                return text;
            }
            else
            {
                string matchingVariable = match.Groups[0].Value.ToString();
                var replacement = Regex.Replace(text, matchingVariable, FindValueByVariable(matchingVariable, variables));
                
                return ExtractVariableValues(replacement, variables);
            }

        }

        public static string FindValueByVariable(string variableName, List<Variable> variables)
        {
            Variable result = variables.FirstOrDefault(variable => variable.Name == variableName);

            if(result == null) {
                //--No variable with this name, Return empty string to avoid weirdness
                return "";
            }
            return result.Value;
            
        }



        public static void TestLoop(TemplateStructure template)
        {
            int count = 1;
            foreach(Layer layer in template.Layers)
            {
                ImageLayer il = layer as ImageLayer;
                if (il != null)
                {
                    // Handle adding Image layers to the canvas here
                    System.Diagnostics.Debug.WriteLine("ITS A IMAGE LAYER");
                    System.Diagnostics.Debug.WriteLine(il);
                    System.Diagnostics.Debug.WriteLine(count);
                }
                TextLayer tl = layer as TextLayer;
                if (tl != null)
                {
                    // Handle adding Text layers to the canvas here
                    System.Diagnostics.Debug.WriteLine("ITS A TEXT LAYER");
                    System.Diagnostics.Debug.WriteLine(tl);
                    System.Diagnostics.Debug.WriteLine(count);
                }

                ShapeLayer sl = layer as ShapeLayer;
                if (sl != null)
                {
                    // Handle adding Shape layers to the canvas here
                    System.Diagnostics.Debug.WriteLine("ITS A SHAPE LAYER");
                    System.Diagnostics.Debug.WriteLine(sl);
                    System.Diagnostics.Debug.WriteLine(count);
                }

                count++;
            }


        }

		//public static async Task<SKData> GenerateFromTemplate(TemplateStructure template, ShopifyProduct productData)
		//{
  //          // Create the base from the width and height
  //          var imgData = productData.Images[0];
  //          bool productImageIsLandscape = imgData.Width >= imgData.Height;
            
  //          SKBitmap imageBg = await GetImageFromUrl(imgData.Src);
  //          SKBitmap scaledImageBg = scaleImageBg(imageBg, imgData, template.Width, template.Height, productImageIsLandscape);

  //          SKSurface surface = SKSurface.Create(new SKImageInfo(template.Width, template.Height));
  //          SKCanvas canvas = surface.Canvas;

  //          // TO center image
  //          float imgX = 0;
  //          float imgY = 0;
  //          if (productImageIsLandscape)
  //          {
  //              imgX = -Math.Abs(((float)scaledImageBg.Width - (float)template.Width) / (float)2);
  //              //float imgX = ((float)scaledImageBg.Width - (float)template.Width) / (float)2;
  //              //float NegX = -Math.Abs(imgX);
  //          } else
  //          {
  //              imgY = -Math.Abs(((float)scaledImageBg.Height - (float)template.Height) / (float)2);
  //          }

  //          // Actually drawing the image to the canvas
  //          canvas.DrawBitmap(scaledImageBg, imgX, imgY);



  //          foreach (Layer l in template.Layers)
  //          {
  //              if(l.LayerType == "TextLayer")
  //              {
  //                 //canvas = ApplyTextLayerToCanvas(canvas, l);
  //              }
  //              System.Diagnostics.Debug.WriteLine(l.LayerType);
  //              System.Diagnostics.Debug.WriteLine(l);
  //          }


  //          // Print actual image
  //          SKImage image = surface.Snapshot();
  //          SKBitmap bitmap = SKBitmap.FromImage(image);
  //          SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100);

  //          // 
  //          //SaveImage(data, "generated.png");

  //          return data;
  //      }
            
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


        public static SKBitmap scaleImageBg(SKBitmap bitmap, double resizeFactor)
        {
            //// factor = desired width / actual width or desired height / actual height
            //double resizeFactor = productImageIsLandscape ? (double)((decimal)templateHeight / (decimal)imageData.Height) : (double)((decimal)templateWidth / (decimal)imageData.Width);

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

