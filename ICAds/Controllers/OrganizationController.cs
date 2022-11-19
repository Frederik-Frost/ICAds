using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ICAds.Data.DTO;
using ICAds.Data.Models;
using ICAds.Data;
using ICAds.Data.Repositories;

namespace ICAds.Controllers
{
    [Route("[controller]")]
    public class OrganizationController : Controller
    {
        
        [HttpPost]
        public async Task<ActionResult<UserModel>> CreateOrganization([FromBody]OrganizationDTO request)
        {
            
            // Check if email
            if (!EmailValidation.IsValidEmail(request.User.Email)) return BadRequest("Not an email");

            using (var db = new AppDataContext())
            {
                // Check if email exists
                UserModel userCheck = await UserRepository.GetUserWithContext(request.User.Email, db);
                if (userCheck != null) return BadRequest("User already exists");
                else
                {
                    var organizationUser = await OrganizationRepository.CreateOrganization(request, db);
                    //var user = await UserRepository.CreateUser(request.User, db);
                    return organizationUser;                  
                }
            }
        }

 
    }
}

