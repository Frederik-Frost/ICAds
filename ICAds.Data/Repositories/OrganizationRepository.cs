using System;
using ICAds.Data.Models;
using ICAds.Data.DTO;
using ICAds.Security;

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

    }
}
