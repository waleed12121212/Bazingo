using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Bazingo_Application.DTOs;
using Bazingo_Core.Models;
using System.Linq;
using Bazingo_Application.Services;
using Bazingo_Application.DTOs.Users;

[ApiController]
[Route("api/[controller]")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService)
    {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser([FromBody] UserRegisterDTO userRegisterDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        var user = new User
        {
            FirstName = userRegisterDTO.FirstName ,
            LastName = userRegisterDTO.LastName ,
            Email = userRegisterDTO.Email
        };

        await _userService.AddUserAsync(user);
        return Ok(new { message = "User registered successfully." });
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserLoginDTO userLoginDTO)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);

        // Implement login logic
        return Ok(new { message = "User logged in successfully." });
    }

    [HttpGet("profile/{id}")]
    public async Task<IActionResult> GetUserProfile(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        return Ok(new UserProfileDTO
        {
            FirstName = user.FirstName ,
            LastName = user.LastName ,
            Email = user.Email
        });
    }

    [HttpGet("all")]
    public async Task<IActionResult> GetAllUsers( )
    {
        var users = await _userService.GetAllUsersAsync();
        return Ok(users.Select(u => new UserProfileDTO
        {
            FirstName = u.FirstName ,
            LastName = u.LastName ,
            Email = u.Email
        }));
    }

    [HttpPut("block/{id}")]
    public async Task<IActionResult> AdminBlockUser(string id)
    {
        var user = await _userService.GetUserByIdAsync(id);
        if (user == null) return NotFound();

        user.IsVerified = false;
        await _userService.UpdateUserAsync(user);

        return Ok(new { message = "User blocked successfully." });
    }
}
