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
            var users = await _repo.GetAllAsync();
            var result = _mapper.Map<List<UserDto>>(users);
            return result;
        }

        public async Task<User?> GetUserById(Guid id)
        {
            var result = await _repo.GetUserById(id);
            if (result == null)
                throw new Exception("Not found user");
            return result;
        }

        public async Task<User?> CreateUser(UserDto newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Id = Guid.NewGuid();
            var result = await _repo.CreateAsync(user);
            if (result == null)
                throw new Exception("Can not create new user");
            return result;
        }
        public async Task<User?> UpdateUser(UserDto updatedUser, Guid id)
        {
            var exitUser = await _repo.GetUserById(id);
            var user = _mapper.Map(updatedUser, exitUser);
            var result = await _repo.UpdateAsync(user);
            if (result == null)
                throw new Exception("Can Not update user");
            return result;
        }

        public async Task DeleteUser(Guid id)
        {
            var user = await _repo.GetUserById(id);
            await _repo.DeleteAsync(user);
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            var result = await _repo.GetByEmail(email);
            if (result == null)
                throw new Exception("User not found");
            return result;
        }

        public async Task<User?> GetUserByEmailAndPassword(string email, string password)
        {
            var result = await _repo.GetByEmailAndPassword(email, password);
            if (result == null)
                throw new Exception("Not found user");
            return result;
        }
    }
}
