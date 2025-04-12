using Application.Settings;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Requests;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetTraining.Controllers.v1
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : BaseV1Controller<UserService, ApplicationSetting>
    {
        private readonly UserService _userService;
        public LoginController(
                IServiceProvider services,
                IHttpContextAccessor httpContextAccessor
                )
                : base(services, httpContextAccessor)
        {
            this._userService = services.GetService<UserService>()!;
        }

        [HttpPost("POST/Sign-up")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var result = await _service.AuthenticateAsync(request);
            return Ok(new
            {
                accessToken = result.accessToken,
                refreshToken = result.refreshToken
            });
        }

        [HttpPost("POST/Sign-in")]
        public async Task<IActionResult> Register([FromBody] UserDto dto)
        {
            try
            {
                var user = await _userService.GetUserByEmail(dto.Email);
                return BadRequest("Email already exists");
            }
            catch (Exception ex)
            {
                if (ex.Message != "Not found user")
                    return Success(await _service.CreateUser(dto));
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("POST/Refresh-Token")]
        public async Task<IActionResult> Refresh([FromBody] RefreshTokenRequest model)
        {
            var (accessToken, refreshToken) = await _service.RefreshTokenAsync(model);
            return Ok(new { accessToken, refreshToken });
        }

        [HttpPost("POST/logout/{email}")]
        public async Task<IActionResult> Logout(string email)
        {
            await _service.LogoutAsync(email);
            return Ok("Logged out.");
        }
    }
}
