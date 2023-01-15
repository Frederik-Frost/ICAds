using System;
using System.Linq;
using System.Drawing;
using System.Net;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using ICAds.Content.Integrations.Shopify;
using ICAds.Content.Models;
using SkiaSharp;
using static System.Net.Mime.MediaTypeNames;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Net.Http.Headers;

namespace ICAds.Content.Image
{
	public class ImageProcessor
	{

        public static async Task<SKData> GenerateFromTemplate(TemplateStructure template, List<Variable> variables)
        {
            // Create canvas with the template width and height first
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

                    // Handle adding Image layers to the canvas here
                    SKBitmap initialImage = await GetImageFromUrl(source);
                    if(initialImage.DrawsNothing != true)
                    {
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

                        float cropPosX = 0;
                        float cropPosY = 0;

                        switch (il.AlignHorizontal)
                        {
                            case "Left":
                                cropPosX = 0;
                                break;
                            case "Center":
                                cropPosX = -Math.Abs(((float)scaledImage.Width - (float)il.Width) / (float)2); 
                                break;
                            case "Right":
                                cropPosX = -Math.Abs(((float)scaledImage.Width - (float)il.Width));
                                break;
                        }

                        switch (il.AlignVertical)
                        {
                            case "Top":
                                cropPosY = 0;
                                break;
                            case "Center":
                                cropPosY =  Math.Abs(((float)scaledImage.Height - (float)il.Height) / (float)2);
                                break;
                            case "Bottom":
                                cropPosY =  Math.Abs(((float)scaledImage.Height - (float)il.Height));
                                break;
                        }

                        SKBitmap cropped = new SKBitmap(new SKImageInfo((int)il.Width, (int)il.Height));
                        scaledImage.ExtractSubset(cropped, SKRectI.Create((int)cropPosX, (int)cropPosY, (int)il.Width, (int)il.Height));

                        // Actually drawing the image to the canvas
                        canvas.DrawBitmap(cropped, il.PosX, il.PosY);
                    } 
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
                    tl.FontWeight = layer.FontWeight;
                    tl.FontWidth = layer.FontWidth;
                    tl.FontSlant = layer.FontSlant;
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


                    SKRect shape = SKRect.Create(sl.PosX, sl.PosY, sl.Width, sl.Height);

                    //SKRect shape = new SKRect();
                    //shape.Offset(sl.PosX, sl.PosY);

                    SKPaint framePaint = new SKPaint
                    {
                        Style = sl.BackgroundStyle == "Fill" ? SKPaintStyle.Fill : SKPaintStyle.Stroke,
                        Color = SKColor.Parse(sl.BackgroundColor ?? "#000")
                    };
                    
                    canvas.DrawRoundRect(shape, sl.BorderRadius, sl.BorderRadius, framePaint);

                }

            }

            // Print actual image
            SKImage image = surface.Snapshot();
            SKBitmap bitmap = SKBitmap.FromImage(image);
            SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100);

            
            //SaveImage(data, "generated.png");

            return data;   
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

        public static SKCanvas ApplyTextLayerToCanvas(SKCanvas canvas, TextLayer layer)
        {
            SKPaint paint = new SKPaint();
            paint.Color = SKColor.Parse(layer.TextColor);
            paint.TextSize = layer.TextSize;
            paint.Typeface = SKTypeface.FromFamilyName(layer.FontFamily, layer.FontWeight, 5, (SKFontStyleSlant)layer.FontSlant);
            paint.IsAntialias = true;
            // Find the text bounds
            SKRect textBounds = new SKRect();
            paint.MeasureText(layer.Text, ref textBounds);

            // makes sure that if small letters go below baseline, then the center is not calculated with that.
            float yIgnorBelowBaseline = layer.PosY - (textBounds.MidY / 2);

            if (layer.HasBackground == true)
            {
                SKRect frameRect = textBounds;
                frameRect.Offset(layer.PosX, (layer.PosY + layer.TextSize));
                frameRect.Inflate(layer.InflateX, layer.InflateY);

                SKPaint framePaint = new SKPaint
                {
                    Style = layer.BackgroundStyle == "Fill" ? SKPaintStyle.Fill : SKPaintStyle.Stroke,
                    Color = SKColor.Parse(layer.BackgroundColor)
                };
                
                canvas.DrawRoundRect(frameRect, layer.BorderRadius, layer.BorderRadius, framePaint);
            }

            canvas.DrawText(layer.Text, layer.PosX, (layer.PosY + layer.TextSize) , paint);
            //canvas.DrawText(layer.Text, layer.PosX, yIgnorBelowBaseline, paint);
            return canvas;
        }



        public static async Task<SKBitmap> GetImageFromUrl(string url)
        {
            HttpClient httpClient = new HttpClient();
            SKBitmap webBitmap;
            if (url == null || url == "") return new SKBitmap();
            try
            {
                using (Stream stream = await httpClient.GetStreamAsync(url))
                using (MemoryStream memStream = new MemoryStream())
                {
                    await stream.CopyToAsync(memStream);
                    memStream.Seek(0, SeekOrigin.Begin);
                    webBitmap = SKBitmap.Decode(memStream);
                    return webBitmap;
                }

            }
            catch (IOException exception)
            {
                throw new IOException("Could not load image");
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

