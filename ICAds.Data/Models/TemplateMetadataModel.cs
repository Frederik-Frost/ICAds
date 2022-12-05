using System;
namespace ICAds.Data.Models
{
	public class TemplateMetadataModel
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsPublic { get; set; }

        public string OrganizationId { get; set; }
        public OrganizationModel Organization { get; set; }
        public string IntegrationId { get; set; }
        public IntegrationModel Integration { get; set; }
        public TemplateModel Template { get; set; }
        public virtual UserModel CreatedByUser { get; set; }
        //public virtual UserModel UpdatedByUser { get; set; }
    }
}

