using System;
using ICAds.Data.Models;
using ICAds.Data.DTO;
using ICAds.Security;
using Microsoft.EntityFrameworkCore;

namespace ICAds.Data.Repositories
{
	public class OrganizationRepository
	{
		public static async Task<UserModel> CreateOrganization(OrganizationDTO orgRequest, AppDataContext db)
		{
            string organizationId = Guid.NewGuid().ToString();
            var hashedPassword = SecurityManager.HashPassword(orgRequest.User.Password);

			OrganizationModel organization = new OrganizationModel();
			organization.Name = orgRequest.OrganizationName;
			organization.Id = organizationId;

            UserModel newUser = new UserModel();
            newUser.Id = Guid.NewGuid().ToString();
            newUser.Firstname = orgRequest.User.Firstname;
            newUser.Lastname = orgRequest.User.Lastname;
            newUser.Email = orgRequest.User.Email;
            newUser.PasswordHash = hashedPassword.Hash;
            newUser.PasswordSalt = hashedPassword.Salt;
            newUser.Organization = organization;

            db.Organizations.Add(organization);
            db.Users.Add(newUser);
            await db.SaveChangesAsync();

            return newUser;
        }

        public static async Task<OrganizationModel> GetOrganizationFromId(string id)
        {
            using (var db = new AppDataContext())
            {
                var org = await db.Organizations.FirstOrDefaultAsync(o =>  o.Id == id);
                return org;
            }
            
        }

        public static async Task<OrganizationModel> UpdateOrganizationData(UpdateOrganizationDTO updateOrganizationDTO)
        {
            using (var db = new AppDataContext())
            {
                var org = await db.Organizations.Where(o => o.Id == updateOrganizationDTO.Id).FirstOrDefaultAsync();
                if(org != null)
                {
                    org.Name = updateOrganizationDTO.Name;
                }
                await db.SaveChangesAsync();
                return org;
            }
        }
        

        public static async Task<List<UserModel>> GetOrganizationUsers(string id)
        {
            using (var db = new AppDataContext())
            {
                List<UserModel> orgUsers = db.Users.Where(u => u.OrganizationId == id).ToList();
                return orgUsers;
            }
        }
    }
}
