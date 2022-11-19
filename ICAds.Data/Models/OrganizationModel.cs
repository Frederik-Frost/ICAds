using System;
using Newtonsoft.Json;
namespace ICAds.Data.Models
{
	public class OrganizationModel
	{
		public string Id { get; set; }
		public string Name { get; set; }

        [JsonIgnore]
        public virtual List<UserModel> Users { get; set; }
        [JsonIgnore]
        public virtual List<IntegrationModel> Integrations { get; set; }
    }
}

