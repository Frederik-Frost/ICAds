using System;
using Microsoft.EntityFrameworkCore;
using ICAds.Data.Models;
using ICAds.Data.DTO;
using Newtonsoft.Json;
using System.Text.Json;

namespace ICAds.Data.Repositories
{
	public class TemplateRepository
	{
        public static async Task<List<TemplateMetadataModel>> GetTemplates(string id)
        {
            using (var db = new AppDataContext())
            {
                var templates = await db.TemplatesMetadata.Where(t => t.OrganizationId == id).ToListAsync();
                return templates;
            }

        }


        public static async Task<TemplateMetadataModel> CreateTemplate(string organizationId, string userId, TemplateDTO templateInfo)
        {
            var user = await UserRepository.GetUserById(userId);

            TemplateMetadataModel templateMetadata = new TemplateMetadataModel();
            templateMetadata.Id = Guid.NewGuid().ToString();
            templateMetadata.Name = templateInfo.Name;
            templateMetadata.CreatedBy = userId;
            templateMetadata.Created = DateTime.UtcNow;
            templateMetadata.UpdatedBy = userId;
            templateMetadata.IsPublic = false;
            templateMetadata.OrganizationId = organizationId;
            templateMetadata.IntegrationId = templateInfo.IntegrationId;
            //templateMetadata.CreatedByUser = user;

            TemplateModel template = new TemplateModel();
            template.Id = Guid.NewGuid().ToString();
            template.TemplateJSON = JsonDocument.Parse("{}");
            template.TemplateMetadataId = templateMetadata.Id;

            using (var db = new AppDataContext())
            {
                db.TemplatesMetadata.Add(templateMetadata);
                db.Templates.Add(template);
                
                await db.SaveChangesAsync();

                return templateMetadata;
            }
        }

        public static async Task<Boolean> DeleteTemplate(string orgId, string templateId)
        {
            using (var db = new AppDataContext())
            {
                var templateMetadata = await db.TemplatesMetadata.FirstOrDefaultAsync(t => t.Id == templateId);
                if (templateMetadata == null) return false;
                db.TemplatesMetadata.Remove(templateMetadata);
                await db.SaveChangesAsync();
                return true;
            }

        }

        public static async Task<TemplateMetadataModel> UpdateTemplateMetadata(string organizationId, string templateId, TemplateDTO templateInfo)
        {
            using (var db = new AppDataContext())
            {
                var existing = await db.TemplatesMetadata.FirstOrDefaultAsync(t => t.Id == templateId && t.OrganizationId == organizationId);
                if(existing != null)
                {
                    existing.Name = templateInfo.Name;
                    existing.IntegrationId = templateInfo.IntegrationId;
                }
                await db.SaveChangesAsync();
                return existing;
            }
        }

        public static async Task<TemplateMetadataModel> GetTemplate(string orgId, string templateId)
        {
            using (var db = new AppDataContext())
            {

                var query = await db.TemplatesMetadata.Where(td => td.Id == templateId && td.OrganizationId == orgId)
                    .Include(td => td.Template)
                    .Include(td => td.Integration)
                    .FirstOrDefaultAsync();

                return query; 

                //var templateMetadata = await db.TemplatesMetadata.FirstOrDefaultAsync(td => td.Id == templateId && td.OrganizationId == orgId);
                //var template = await db.Templates.FirstOrDefaultAsync(t => t.TemplateMetadataId == templateId);

            }

        }

        public static async Task<TemplateMetadataModel> SaveTemplateJson(string orgId, string userId, string templateId, JsonDocument templateJson)
        {
            using (var db = new AppDataContext())
            {

                var existing = await db.TemplatesMetadata.Where(td => td.Id == templateId && td.OrganizationId == orgId)
                    .Include(td => td.Template)
                    .FirstOrDefaultAsync();
                if(existing != null)
                {
                    existing.Template.TemplateJSON = templateJson;
                    existing.UpdatedBy = userId;
                    existing.Updated = DateTime.UtcNow;
                }
                await db.SaveChangesAsync();
                return existing;
            }

        }

        //public static async Task<IntegrationModel> GetTemplateIntegration(string orgId, string templateId)
        //{

        //}


        public class NewTemplate
        {
            public Dictionary<string, string> Name { get; set; }
        }
    }
}

