using System.Text;
using Application.Settings;
//using BPMaster.Services;
using Common.Controllers;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Requests;
using DotNetTraining.Services;
using Microsoft.AspNetCore.Authorization;
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

    [HttpGet("GET/users")]
    public async Task<IActionResult> GetAllUsers(
        [FromQuery] PaginationFilter pagination,
        [FromQuery] SortingFilter sorting,
        [FromQuery] FilteringFilter filtering)
    {
        // Gọi phương thức GetAllUsers từ UserService
        var result = await _userService.GetAllUsers(pagination, sorting, filtering);
        return Success(result);
    }

    [HttpGet("GET/user/{id}")]
    public async Task<IActionResult> GetUserById(Guid id)
    {  
            var user = await _userService.GetUserById(id);
            return Success(user);
    }

    [HttpPost("POST/users")]
    public async Task<IActionResult> CreateUser([FromBody] UserDto dto)
    {
        return CreatedSuccess(await _service.CreateUser(dto));
    }

    [HttpPut("PUT/users/{id}")]
    public async Task<IActionResult> UpdateUser([FromBody] UserDto dto, Guid id)
    {
        return Success(await _service.UpdateUser(dto, id));
    }

    [HttpDelete("DELETE/users/{id}")]
    public async Task<IActionResult> DeleteUser(Guid id)
    {
        await _service.DeleteUser(id);
        return Success("User deleted successfully");
    }
}
