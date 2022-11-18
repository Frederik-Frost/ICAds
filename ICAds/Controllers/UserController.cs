using Microsoft.AspNetCore.Mvc;
using ICAds.Data.DTO;
using ICAds.Data.Models;
using ICAds.Data.Repositories;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace ICAds.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController: ControllerBase
{

    public static UserModel user = new UserModel();

    [Route("register")]
    [HttpPost]
    public async Task<ActionResult<UserModel>> Register([FromBody] UserDTO request)
    {
        //var user = UserRepository.CreateUser(request);
        user = UserRepository.CreateUser(request);

        return Ok(user);
    }
    [Route("login")]
    [HttpPost]
    public async Task<IActionResult> Login([FromBody] LoginDTO request)
    {

        // Check if user exists
        if (user.Email != request.Email)
        {
            return BadRequest("User not found");
        }

        var token = UserRepository.LoginUser(request, user);

        if(token == null)
        {
            return BadRequest("Wrong Password");
        }
        return Ok(token);
    }


    [Route("me")]
    [HttpGet]
    [Authorize]
    public string GetMe()
    {
        // Get User id from JWT - bearer token
        var id = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
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

