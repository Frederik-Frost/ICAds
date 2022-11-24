using System;
using ICAds.Data.DTO;
using ICAds.Data.Models;

namespace ICAds.Data.Repositories
{
	public class IntegrationRepository
	{
		public static async Task<IntegrationModel> CreateIntegration(string orgId, IntegrationDTO integration)
		{
			using (var db = new AppDataContext())
			{
				IntegrationModel newIntegration = new IntegrationModel();
				newIntegration.Id = Guid.NewGuid().ToString();
                newIntegration.Name = integration.Name;
				newIntegration.IntegrationType = 1;
				newIntegration.Url = integration.Url;
				newIntegration.AccessToken = integration.AccesToken;
				newIntegration.OrganizationId = orgId;

				db.Integrations.Add(newIntegration);
				await db.SaveChangesAsync();

				return newIntegration;
			}
		}

        public static async Task<List<IntegrationModel>> GetIntegrations(string id)
        {
            using (var db = new AppDataContext())
            {
				return db.Integrations.Where(i => i.OrganizationId == id).ToList();
            }

        }

        public static async Task<IntegrationModel> GetIntegrationById(string id)
		{
			using (var db = new AppDataContext())
			{
				return db.Integrations.FirstOrDefault(i => i.Id == id);
			}
			
		}
	}
}

