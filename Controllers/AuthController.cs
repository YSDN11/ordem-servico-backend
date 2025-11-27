using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ordem_servico_backend.Models;
using ordem_servico_backend.Services.Implementation;
using ordem_servico_backend.Services.Interface;
using ordem_servico_backend.Utils;

namespace ordem_servico_backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IJwtTokenService _jwtService;

        public AuthController(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            IJwtTokenService jwtService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtService = jwtService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto dto)
        {
            var user = new AppUser
            {
                UserName = dto.UserName,
                Email = dto.Email
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return Ok(new { message = "Usuário registrado com sucesso." });
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto dto)
        {
            AppUser? user = await _userManager.FindByNameAsync(dto.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByEmailAsync(dto.UserNameOrEmail);

            if (user == null)
                return Unauthorized(new { message = "Credenciais inválidas." });

            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
                return Unauthorized(new { message = "Credenciais inválidas." });

            var token = _jwtService.GenerateToken(user);

            return Ok(new
            {
                token,
                user = new
                {
                    id = user.Id,
                    userName = user.UserName,
                    email = user.Email
                }
            });
        }
    }
}
