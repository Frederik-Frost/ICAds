using Microsoft.AspNetCore.Mvc;
using ICAds.Data.DTO;
using ICAds.Data.Models;
using ICAds.Data.Repositories;
using ICAds.Data;
using System.Security.Claims;

using Microsoft.AspNetCore.Authorization;

namespace ICAds.Controllers;

[ApiController]
[Route("users")]
public class UserController: TokenController
{

    //public static UserModel user = new UserModel();

    [Route("register")]
    [HttpPost]
    public async Task<ActionResult<UserModel>> Register([FromBody] UserDTO request)
    {
        if (!EmailValidation.IsValidEmail(request.Email)) return BadRequest("Not an email");

        using (var db = new AppDataContext())
        {
            UserModel userCheck = await UserRepository.GetUserWithContext(request.Email, db);
            if (userCheck != null) return BadRequest("User already exists");
            else
            {
                var user = await UserRepository.CreateUser(request, db);
                return user;
            }
        }


    }

    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDTO request)
    {
        var token = await UserRepository.LoginUser(request);
        if (token == null)  return StatusCode(400, new { Message = "Username or password is incorrect" });
        return Ok(token);
    }

    [Route("me")]
    [HttpGet]
    [Authorize]
    public async Task<ActionResult<UserModel>> GetMe()
    {
        // Get User id from JWT - bearer token
        var user = await UserRepository.GetUserById(GetUserId());
        return Ok(user);
    }

    [Route("org")]
    [HttpGet]
    [Authorize]
    public string GetOrg()
    {
        // Get Organization id from JWT - bearer token
        var id = User.Claims.FirstOrDefault(c => c.Type == "OrganizationId")?.Value;
        return id;
    }
    

    [Route("all")]
    [HttpGet]
    public async Task<List<UserModel>> GetAll()
    {
        var users = UserRepository.GetAllUsers();
        return users;
    }
}

