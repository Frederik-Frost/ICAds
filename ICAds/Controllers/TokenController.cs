using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
namespace ICAds.Controllers
{
    
    public abstract class TokenController : ControllerBase
    {
        protected string GetUserId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
        }

        protected string GetOrgId()
        {
            return User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;
        }
    }
}

