using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace ICAds.Data.Models
{
	public class TemplateModel
	{
        public string Id { get; set; }
        [Column(TypeName = "jsonb")]
        public JsonDocument TemplateJSON { get; set; }

        public string TemplateMetadataId { get; set; }
        public TemplateMetadataModel TemplateMetadata { get; set; }

    }
}

