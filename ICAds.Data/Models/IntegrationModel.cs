using System;
namespace ICAds.Data.Models
{
	public class IntegrationModel
	{
        public int IntegrationType { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string AccessToken { get; set; }

        public string OrganizationId { get; set; }
        public OrganizationModel Organization { get; set; }
        public List<TemplateMetadataModel> TemplateMetadata { get; set; }
    }
}

