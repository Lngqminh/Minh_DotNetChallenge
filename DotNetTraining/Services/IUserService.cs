using DotNetTraining.Domains.Dtos;

namespace DotNetTraining.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(CreateUserDto userDto);
        Task UpdateUserAsync(int id, UpdateUserDto userDto);
        Task DeleteUserAsync(int id);   
    }
}
