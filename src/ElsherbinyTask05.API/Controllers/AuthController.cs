using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ElsherbinyTask05.API.Constants;
using ElsherbinyTask05.API.Contracts.Req;
using ElsherbinyTask05.API.Models;
using ElsherbinyTask05.API.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace ElsherbinyTask05.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{

    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;




    public AuthController(IConfiguration config, IUserRepository userRepository)
    {
        _config = config;
        _userRepository = userRepository;
    }

    private string GenerateToken(User data)
    {
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetValue<string>("JWT:SecretKey")!));


        var signingCredentiels = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);

        List<Claim> Claims = [];
        Claims.Add(new(JwtRegisteredClaimNames.UniqueName, data.Username));
        Claims.Add(new(ClaimTypes.Role, data.Role));
        Claims.Add(new(JwtRegisteredClaimNames.Name, data.FullName));

        var token = new JwtSecurityToken(
            _config.GetValue<string>("JWT:Issuer"),
            _config.GetValue<string>("JWT:Audience"),
            Claims,
            DateTime.Now,
             DateTime.Now.AddHours(12)
             , signingCredentiels
        );


        return new JwtSecurityTokenHandler().WriteToken(token);

    }


    [HttpPost("token")]
    [AllowAnonymous]
    public IActionResult GetToken(UserDtoReq data)
    {
        var user = _userRepository.GetByUsername(data.Username);

        if (user is null || user.Password != data.Password)
            return Unauthorized();

        var token = GenerateToken(user);
        return Ok(new { token });
    }



    [HttpPost("signup")]
    [AllowAnonymous]
    public IActionResult Signup(UserSignupDtoReq data)
    {
        if (string.IsNullOrWhiteSpace(data.Username) || string.IsNullOrWhiteSpace(data.Password))
            return BadRequest("Username and password are required.");

        // var existingUser = _userRepository.GetByUsername(data.Username);
        // if (existingUser != null)
        //     return Conflict("Username already exists.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Username = data.Username,
            Password = data.Password,
            FullName = data.FullName ?? data.Username,
            Role = RolesConstants.User
        };

        _userRepository.Add(user);

        var token = GenerateToken(user);
        return Ok(new { token });
    }



    [HttpPost("changerole")]
    [Authorize(Roles = RolesConstants.Admin)]
    public IActionResult ChangeUserRole([FromBody] ChangeUserRoleDto data)
    {
        if (string.IsNullOrWhiteSpace(data.Username) || string.IsNullOrWhiteSpace(data.NewRole))
            return BadRequest("Username and new role are required.");

        var user = _userRepository.GetByUsername(data.Username);
        if (user == null)
            return NotFound("User not found.");

        user.Role = data.NewRole;
        _userRepository.Update(user);

        return Ok(new { message = "User role updated successfully." });
    }

    
}




// List<User> users = [
//     new User {
//     FullName="Adnane Malki",
//     Role = "Admin",
//     Username = "hexatox",
//     Password ="Admin!",
//     Id = Guid.NewGuid()    },
//      new User {
//     FullName="Hashim Khalid",
//     Role = "user",
//     Username = "hashim",
//     Password ="User!",
//     Id = Guid.NewGuid()    },
//      new User {
//     FullName="Adnane Editor",
//     Role = "editor",
//     Username = "hexaEditor",
//     Password ="Editor!",
//     Id = Guid.NewGuid()    }

// ]; 