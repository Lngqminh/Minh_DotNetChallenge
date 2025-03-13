using Application.Settings;
using System.Text;
using AutoMapper;
using Common.Application.CustomAttributes;
using Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;
using Newtonsoft.Json;
using System.Data;

namespace DotNetTraining.Services
{
    //public class UserService : IUserService
    //{
    //    private readonly IUserRepository _userRepository;
    //    private readonly IMapper _mapper;

    //    public UserService(IUserRepository userRepository, IMapper mapper)
    //    {
    //        _userRepository = userRepository;
    //        _mapper = mapper;
    //    }

    //    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    //    {
    //        var users = await _userRepository.GetAllUsersAsync();
    //        return _mapper.Map<IEnumerable<UserDto>>(users);
    //    }

    //    public async Task<UserDto?> GetUserByIdAsync(int id)
    //    {
    //        var user = await _userRepository.GetUserByIdAsync(id);
    //        return user == null ? null : _mapper.Map<UserDto>(user);
    //    }

    //    public async Task<UserDto> CreateUserAsync(CreateUserDto userDto)
    //    {
    //        var user = _mapper.Map<User>(userDto);
    //        var createdUser = await _userRepository.CreateUserAsync(user);
    //        return _mapper.Map<UserDto>(createdUser);
    //    }

    //    public async Task UpdateUserAsync(int id, UpdateUserDto userDto)
    //    {
    //        var user = await _userRepository.GetUserByIdAsync(id);
    //        if (user == null) return;

    //        _mapper.Map(userDto, user);
    //        await _userRepository.UpdateUserAsync(user);
    //    }

    //    public async Task DeleteUserAsync(int id) => await _userRepository.DeleteUserAsync(id);
    //}
    [ScopedService]
    public class UserService(IServiceProvider services,
        ApplicationSetting setting,
        IDbConnection connection): BaseService(services)
    {
        private readonly UserRepository _repo = new(connection);
           public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllUsersAsync();
            return users;
            
        }




    }
}
