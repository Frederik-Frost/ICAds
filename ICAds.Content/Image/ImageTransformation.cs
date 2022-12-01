using System;
using System.Diagnostics;
using System.Drawing;
using System.Reflection.PortableExecutable;
using SkiaSharp;
using SkiaSharp.Views.Forms;

namespace ICAds.Content.Image
{
	public class ImageTransformation
    {

		public static async Task<SKBitmap> ScaleImage()
		{


            float resizeFacotor = 0.24f;
            SKBitmap bitmap = await GetImage();
            

            int width = (int)Math.Round(bitmap.Width * resizeFacotor);
            int height = (int)Math.Round(bitmap.Height * resizeFacotor);

            SKBitmap bitmap2 = new SKBitmap(width, height, bitmap.ColorType, bitmap.AlphaType);

            SKCanvas canvas = new SKCanvas(bitmap2);

            canvas.SetMatrix(SKMatrix.CreateScale(resizeFacotor, resizeFacotor));
            canvas.DrawBitmap(bitmap, new SKPoint());
            canvas.ResetMatrix();
            canvas.Flush();


            return bitmap2;
            //SKImage sKImage = SKImage.FromBitmap(bitmap2);
            //SKData sKData = sKImage.Encode(SKEncodedImageFormat.Png, 100);

            //SaveImage(sKData, $"resized_{resizeFacotor}.png");

        }

        public static async void PaintOnImage()
		{
            //SKBitmap imageBg = await GetImage();
            SKBitmap imageBg = await ScaleImage();

            SKSurface surface = SKSurface.Create(new SKImageInfo(800, 800));
            SKCanvas canvas = surface.Canvas;
            SKPaint paint = new SKPaint();
            string text = @"Orange web";
            float x = 10;
            float y = 50;

			float imgY = 0;

            // -200 because width is 1200 ----> 1200 - 800 = 400px for leftovers. 400 / 2 equals the offset to find the center placement.
			float imgX = -200;

            canvas.DrawBitmap(imageBg, imgX, imgY);
            //canvas.DrawBitmap(scaledImage, imgX, imgY);
			

            paint.Color = SKColor.Parse("#F6A21C");
            //paint.Color = SKColors.Blue;
            paint.TextSize = 40;
            paint.Typeface = SKTypeface.FromFamilyName("Roboto");
            paint.IsAntialias = true;


			canvas.DrawText(text, x, y, paint);



            SKImage image = surface.Snapshot();
            SKBitmap bitmap = SKBitmap.FromImage(image);
            SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100);

            SaveImage(data, "Testdata9.png");

        }

        public static async void PaintOnImage2()
		{
			SKBitmap imageBg = await GetImage();

			using(SKCanvas canvas = new SKCanvas(imageBg))
			{
			
				SKPaint paint = new SKPaint();
				string text = @"Orange web";
				float x = 10;
				float y = 50;
				
				paint.Color = SKColor.Parse("#F6A21C");
				//paint.Color = SKColors.Blue;
				paint.TextSize = 40;
				paint.Typeface = SKTypeface.FromFamilyName("Roboto");
				paint.IsAntialias = true;

				canvas.DrawText(text, x, y, paint);

                //            SKImage image = surface.Snapshot();
                //            //SKBitmap bitmap = SKBitmap.FromImage(image);
                //SKImage img = canvas.SnapShot
                //            SKBitmap bitmap = SKBitmap.FromImage(canvas);
                //            SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100);

                //SKCanvasView canvasView = new SKCanvasView();
                //can

		


                //var data = canvas.Encode(SKEncodedImageFormat.Png, 100);
                //SaveImage(data, "Testdata7.png");

            }





        }


		public static async Task<SKBitmap> GetImage()
		{
            HttpClient httpClient = new HttpClient();
            SKBitmap webBitmap;
			//string url = "https://cdn.shopify.com/s/files/1/0453/3603/5496/products/61OmPjkXxUL._MCnd_AC_UL320.jpg?v=1660045731";
			string url = "https://cdn.shopify.com/s/files/1/0453/3603/5496/products/distressed-kids-jeans_36bdad7a-db05-4366-ab7d-0bc59cdc744d.jpg?v=1629360667";
            using (Stream stream = await httpClient.GetStreamAsync(url))
            using (MemoryStream memStream = new MemoryStream())
            {
                await stream.CopyToAsync(memStream);
                memStream.Seek(0, SeekOrigin.Begin);
                webBitmap = SKBitmap.Decode(memStream);
				//canvasView.InvalidateSurface();
				//SKData data = webBitmap.Encode(SKEncodedImageFormat.Png, 100);
				return webBitmap;
            }

        }



		public static async void LoadImageFromWeb()
		{
            HttpClient httpClient = new HttpClient();   
            //SKCanvasView canvasView;
            SKBitmap webBitmap;

            //canvasView = new SKCanvasView();

            string url = "https://cdn.shopify.com/s/files/1/0453/3603/5496/products/61OmPjkXxUL._MCnd_AC_UL320.jpg?v=1660045731";

			try
			{
				using (Stream stream = await httpClient.GetStreamAsync(url))
				using (MemoryStream memStream = new MemoryStream()){
					await stream.CopyToAsync(memStream);
					memStream.Seek(0, SeekOrigin.Begin);
					webBitmap = SKBitmap.Decode(memStream);
					//canvasView.InvalidateSurface();
                    SKData data = webBitmap.Encode(SKEncodedImageFormat.Png, 100);

                    SaveImage(data, "Testdata5.png");
                }
			}
			catch
			{

			}

        }


        public static void CreateTextImageWithBg()
        {
            string text = @"Orange web";
			float x = 10;
			float y = 50;



            SKSurface surface = SKSurface.Create(new SKImageInfo(300, 300));
            SKCanvas canvas = surface.Canvas;
            SKPaint paint = new SKPaint();


            paint.Color = SKColor.Parse("#F6A21C");
            //paint.Color = SKColors.Blue;
            paint.TextSize = 40;
            paint.Typeface = SKTypeface.FromFamilyName("Roboto");
            paint.IsAntialias = true;

            // Find the text bounds
            SKRect textBounds = new SKRect();
            paint.MeasureText(text, ref textBounds);


			System.Diagnostics.Debug.WriteLine(textBounds);
            System.Diagnostics.Debug.WriteLine(textBounds.MidX);
            System.Diagnostics.Debug.WriteLine(textBounds.MidY);

			//float yText = x / 2  -  textBounds.MidY;
			//float xText = y / 2- textBounds.MidX;

			// makes sure that if small letters go below baseline, then the center is not calculated with that.
			float yIgnorBelowBaseline = y - (textBounds.MidY / 2);
           

            SKRect frameRect = textBounds;
			frameRect.Offset(x, y);
			frameRect.Inflate(10, 10);


			SKPaint framePaint = new SKPaint
			{
				Style = SKPaintStyle.Fill,
				Color = SKColor.Parse("#ededed")
			};

			canvas.DrawRoundRect(frameRect, 20, 20, framePaint);
            canvas.DrawText(text, x, yIgnorBelowBaseline, paint);


			

            SKImage image = surface.Snapshot();
            SKBitmap bitmap = SKBitmap.FromImage(image);
            SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100);


			SaveImage(data, "Testdata3.png");

		}

		public static void CreateTextImage()
		{
			string text = @"Orange web";

			SKSurface surface = SKSurface.Create(new SKImageInfo(300, 300));
			SKCanvas canvas = surface.Canvas;
			SKPaint paint = new SKPaint();


			paint.Color = SKColor.Parse("#F6A21C");
            //paint.Color = SKColors.Blue;
			paint.TextSize = 60;
			paint.Typeface = SKTypeface.FromFamilyName("Roboto");
			paint.IsAntialias = true;
		
			canvas.DrawText(text, 10, 50, paint);

			SKImage image = surface.Snapshot();
			SKBitmap bitmap = SKBitmap.FromImage(image);
			SKData data = bitmap.Encode(SKEncodedImageFormat.Png, 100);

			SaveImage(data, "Testdata2.png");

		}



		public static void CreateBlankImage()
		{

			// new bitmap in the size of 300 x 300 px
			SKBitmap bitmap = new SKBitmap(300, 300);


			// image type as png and quality 100 %
			SKData imageData = 	bitmap.Encode(SKEncodedImageFormat.Png, 100);

			using (FileStream file = File.Create(@"imagetest.png"))
			{
				imageData.AsStream().Seek(0, SeekOrigin.Begin);
				imageData.AsStream().CopyTo(file);
				file.Close();
				file.Flush();
			}
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

