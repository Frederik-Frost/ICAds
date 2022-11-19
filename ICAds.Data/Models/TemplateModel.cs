using System;
namespace ICAds.Data.Models
{
	public class TemplateModel
	{
        public string Id { get; set; }
        public string Name { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsPublic { get; set; }
        public string TemplateJSON { get; set; }

        public string OrganizationId { get; set; }
        public OrganizationModel Organization { get; set; }
    }
}

