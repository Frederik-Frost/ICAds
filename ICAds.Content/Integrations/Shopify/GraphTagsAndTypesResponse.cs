using System;
namespace ICAds.Content.Integrations.Shopify
{
	public class GraphTagsAndTypesResponse
	{
        public Dictionary<string, GraphDictionary> Data { get; set; }
    }
    public class GraphDictionary
    {
        public EdgesResponse ProductTags { get; set; }
        public EdgesResponse ProductTypes { get; set; }
    }

    public class EdgesResponse
    {
        public List<CollectionNode> Edges { get; set; }
    }

    public class CollectionNode
    {
        public string Node { get; set; }
    }
}

