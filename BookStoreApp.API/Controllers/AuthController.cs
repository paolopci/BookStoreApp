using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.User;
using BookStoreApp.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApiUser> _userManager;
        private readonly TokenService _tokenService;
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;

        public AuthController(UserManager<ApiUser> userManager, TokenService tokenService,
                              ILogger<AuthController> logger, IMapper mapper)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _logger = logger;
            _mapper = mapper;

        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserDto userDto)
        {
            _logger.LogInformation("Registering user with email: {Email}", userDto.Email);
            try
            {
                var user = _mapper.Map<ApiUser>(userDto);
                var result = await _userManager.CreateAsync(user, userDto.Password);

                if (!result.Succeeded)
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("RegistrationError", error.Description);
                    }
                    return BadRequest(ModelState);
                }

                await _userManager.AddToRoleAsync(user, "User");

                return Accepted();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An error occurred while registering the user in the {nameof(Register)}");
                return Problem($"An error occurred while registering the user in the {nameof(Register)}",
                    statusCode: 500);
            }

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login(LoginUserDto userDto)
        {
            _logger.LogInformation("Logging in user with email: {Email}", userDto.Email);
            try
            {
                var user = await _userManager.FindByEmailAsync(userDto.Email);
                if (user == null)
                {
                    return NotFound("User not found");
                }
                var passwordValid = await _userManager.CheckPasswordAsync(user, userDto.Password);
                if (!passwordValid)
                {
                    return Unauthorized("Credenziali non valide");
                }

                // 8. Generazione del token tramite TokenService
                var tokenString = await _tokenService.GenerateTokenAsync(user);




                return Ok(new { Token = tokenString });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Errore durante il login dell'utente nel metodo {Method}", nameof(Login));
                return Problem(detail: "Si è verificato un errore interno durante il login", statusCode: 500);
            }
        }

    }
}
