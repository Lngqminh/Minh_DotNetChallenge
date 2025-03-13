using System.Text;
using Application.Settings;
//using BPMaster.Services;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Mvc;

[Route("api/users")]
[ApiController]
public class UsersController : BaseV1Controller<UserService, ApplicationSetting>
{
    private readonly UserService _userService;
    public UsersController(
            IServiceProvider services,
            IHttpContextAccessor httpContextAccessor
   
            )
            : base(services, httpContextAccessor)
    {
        this._userService = services.GetService<UserService>()!;
    }


    [HttpGet("getAllUsers")]
    public async Task<IActionResult> GetAllUsers()
    {
        var user = await _userService.GetAllUsers();
        return Success(user);
    }

    [HttpPost("create")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
    {
        return CreatedSuccess(await _service.CreateUser(dto));
    }
}
