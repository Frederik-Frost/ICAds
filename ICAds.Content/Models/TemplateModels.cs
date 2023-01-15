using System;
//using System.Text.Json;
using Newtonsoft.Json;
using ICAds.Content.Integrations.Shopify;

namespace ICAds.Content.Models
{
    public class TemplateStructure
    {
        public int Height { get; set; }
        public int Width { get; set; }
        public List<Layer> Layers { get; set; }
    }

    public class TemplateStructureDTO
    {
        public int Height { get; set; }
        public int Width { get; set; }
        //public JsonDocument Layers { get; set; }
    }

    public class Layer
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
        public string Source { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public string ObjectFit { get; set; }
        public string AlignHorizontal { get; set; }
        public string AlignVertical { get; set; }
        public int FontWeight { get; set; }
        public int FontWidth { get; set; }
        public int FontSlant { get; set; }

    }

	public class TextLayer : Layer
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
        public int FontWeight { get; set; }
        public int FontWidth { get; set; }
        public int FontSlant { get; set; }
    }

	public class ImageLayer : Layer
    {
     
        public string LayerType { get; set; }
        public string Source { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public string ObjectFit { get; set; }
        public string AlignHorizontal { get; set; }
        public string AlignVertical { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
    }


    public class ShapeLayer : Layer
    {
        public string LayerType { get; set; }
        public float Height { get; set; }
        public float Width { get; set; }
        public string BackgroundColor { get; set; }
        public string BackgroundStyle { get; set; }
        public int BorderRadius { get; set; }
        public float PosX { get; set; }
        public float PosY { get; set; }
    }


    public class GenerateTemplateDTO
    {
        public TemplateStructure Template { get; set; }
        public List<Variable> Variables { get; set; }
    }

    public class GenerateMultipleTemplateDTO
    {
        public List<GenerateTemplateDTO> Generations { get; set; }
    }



}

