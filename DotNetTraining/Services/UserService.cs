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
using Microsoft.AspNetCore.Identity;

namespace DotNetTraining.Services
{
    [ScopedService]
    public class UserService(IServiceProvider services,ApplicationSetting setting,IDbConnection connection): BaseService(services)
    {
        private readonly UserRepository _repo = new(connection);
        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _repo.GetAllUsers();

            var result = _mapper.Map<List<UserDto>>(users);

            return result;

        }

        public async Task<User?> CreateUser(UserDto newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Id = Guid.NewGuid();

            return await _repo.Create(user);
        }

    }
}
