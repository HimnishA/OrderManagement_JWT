using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using OrderManagementApi.Models;
using OrderManagementApi.DTOs;
using OrderManagementApi.Services;


namespace OrderManagementApi.Controllers;


[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ITokenService _tokenService;


    public AuthController(UserManager<ApplicationUser> userManager, ITokenService tokenService)
    {
        _userManager = userManager;
        _tokenService = tokenService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);


        var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
        var result = await _userManager.CreateAsync(user, model.Password);
        if (!result.Succeeded) return BadRequest(result.Errors);


        var token = _tokenService.CreateToken(user);
        return Ok(new { token });
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto model)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);


        var user = await _userManager.FindByEmailAsync(model.Email);
        if (user == null) return Unauthorized("Invalid credentials");


        var isValid = await _userManager.CheckPasswordAsync(user, model.Password);
        if (!isValid) return Unauthorized("Invalid credentials");


        var token = _tokenService.CreateToken(user);
        return Ok(new { token });
    }
}