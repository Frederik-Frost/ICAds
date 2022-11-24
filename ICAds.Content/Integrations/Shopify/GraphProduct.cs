
using System;

namespace ICAds.Content.Integrations.Shopify
{

    public class GraphProductResponse
    {
        public Dictionary<string, GraphqlEdgesResponse> Data { get; set; }
        public List<GraphError> Errors { get; set; }
    }

    public class GraphqlEdgesResponse
    {
        public List<GraphqlEdge> Edges { get; set; }
    }
    public class GraphqlEdge
    {
        public GraphqlNode Node { get; set; }
    }

    public class GraphqlNode
    {
        //public string Id { get; set; }
        public string Title { get; set; }
        

        string _id;
        public string Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value.Replace($"gid://shopify/Product/", "");
            }
        }
    }

    public class GraphError
    {
        public string Message { get; set; }
        public List<string> Fields { get; set; }
        public List<ErrorLocation> Locations { get; set; }
    }
    public class ErrorLocation
    {
        public int? Line { get; set; }
        public int? Column { get; set; }
    }
}

