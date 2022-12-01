using System;
using ICAds.Content.Integrations.Shopify;

namespace ICAds.Content.Models
{
	public class TemplateStructure
	{
		//public TemplateStructure(TemplateStructure t)
		//{
		//	Height = t.Height;
		//	Width = t.Width;
		//	Layers = t.Layers;

  //      }

		public int Height { get; set; }
		public int Width { get; set; }
		public List<TextLayer> Layers { get; set; } 
    }

	public struct Layer
    {
		public string LayerType { get; set; }
		public TextLayer TextLayer { get; set; }
        public ImageLayer ImageLayer { get; set; }

    }

	public class TextLayer
	{
        public string LayerType { get; set; }
        public string Text { get; set; }
		public float TextSize { get; set; }
		public string TextColor { get; set; }
        public string FontFamily { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
		public Boolean HasBackground { get; set; }
		public float InflateX { get; set; }
        public float InflateY { get; set; }
		public string BackgroundStyle { get; set; }
		public string BackgroundColor { get; set; }
		public int BorderRadius { get; set; }
    }
    public class ImageLayer
    {
        public string Url { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
    }

	


    
    public class GenerateTemplateDTO
    {
        public TemplateStructure Template { get; set; }
        public ShopifyProduct ProductData { get; set; }
    }
    


}

