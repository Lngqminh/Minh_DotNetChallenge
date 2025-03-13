using System.Text;
using Application.Settings;
using BPMaster.Services;
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

    [HttpGet] public async Task<IActionResult> GetUsers() => Ok(await _userService.GetAllUsersAsync());

  

    [HttpGet("test")]
    public async Task<IActionResult> GetAllBP()
    {
        try
        {
            
            string result = "Test";
            Console.WriteLine($"Response: {result}");
            return Success("ok");
        }
        catch (Exception ex)
        {
            return Error(ex.Message);
        }
    }
}
