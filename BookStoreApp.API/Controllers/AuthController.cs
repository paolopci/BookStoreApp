using AutoMapper;
using BookStoreApp.API.Data;
using BookStoreApp.API.Models.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookStoreApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILogger<AuthController> _logger;
        private readonly IMapper _mapper;
        private readonly UserManager<ApiUser> _userManager;

        public AuthController(ILogger<AuthController> logger, IMapper mapper, UserManager<ApiUser> userManager)
        {
            _logger = logger;
            _mapper = mapper;
            _userManager = userManager;
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

        }


    }
}
