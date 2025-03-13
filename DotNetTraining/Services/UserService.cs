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
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DotNetTraining.Services
{
    [ScopedService]
    public class UserService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly UserRepository _repo = new(connection);

        public async Task<List<UserDto>> GetAllUsers()
        {
                var users = await _repo.GetAll();
                var result = _mapper.Map<List<UserDto>>(users);
            return result;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            var result = await _repo.GetById(id);
            if (result == null)
                throw new Exception("Not found user");
            return result;
        }

        public async Task<User?> CreateUser(UserDto newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Id = Guid.NewGuid();
            var result = await _repo.Create(user);
            if (result == null)
                throw new Exception("Can not create new user");
            return result;
        }
        public async Task<User?> UpdateUser(UserDto updatedUser, Guid id)
        {
            var user = _mapper.Map<User>(updatedUser);
            var result = await _repo.Update(user, id);
            if (result == null)
                throw new Exception("Can Not update user");
            return result;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _repo.GetById(id);
            await _repo.Delete(id);
        }
    }
}
