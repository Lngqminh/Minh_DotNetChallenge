using Application.Settings;
using System.Text;
using AutoMapper;
using Common.Application.CustomAttributes;
using Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;
using DotNetTraining.Requests;
using Newtonsoft.Json;
using System.Data;
using Microsoft.AspNetCore.Identity;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.AspNetCore.Http.HttpResults;
using Common.Application.Models;
using Common.Application.Exceptions;
using Common.Application.Settings;
using Common.Utilities;
using Microsoft.Extensions.Configuration;
using Utilities;
using DotNetTraining.Domains.Models;
using Domain.Enums;

namespace DotNetTraining.Services
{
    [ScopedService]
    public class UserService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection, IConfiguration configuration) : BaseService(services)
    {
        private readonly UserRepository _repo = new(connection);
        private readonly IConfiguration _configuration = configuration;
        private readonly JwtTokenSetting _jwtTokenSetting;
        public async Task<List<UserModel>> GetAllUsers()
        {
            var users = await _repo.GetAllAsync();
            var result = _mapper.Map <List<UserModel>>(users);
            return result;
        }

        public async Task<UserModel?> GetUserById(Guid id)
        {
            var result = await _repo.GetUserById(id);
            if (result == null)
                throw new Exception("Not found user");
            //Map model
            var userModel = _mapper.Map<UserModel>(result);
            return userModel;
        }

        public async Task<User?> CreateUser(UserDto newUser)
        {
            var user = _mapper.Map<User>(newUser);
            user.Id = Guid.NewGuid();

            var hasher = new HashingWithKeyService(_configuration);
            user.Password = hasher.HashPassword(newUser.Password);

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
            var result = await _repo.GetByEmailAsync(email);
            if (result == null)
                throw new Exception("User not found");
            return result;
        }

        public async Task<string> AuthenticateAsync(LoginRequest request)
        {
            var user = await _repo.GetByEmailAsync(request.Email);
            if (user == null || user.Status == UserStatus.Deleted)
            {
                throw new NonAuthenticateException("The account does not exist in the system. Please contact the admin to have the account added.");
            }

            if (user.Status != UserStatus.Active)
            {
                throw new NonAuthenticateException("Account is not active. Please contact the administrator.");
            }

            var hashingService = new HashingWithKeyService(_configuration);
            if (hashingService.VerifyPassword(user.Password, request.Password))
            {
                user.LastLoggedIn = DateTime.Now;
                try
                {
                    await _repo.UpdateAsync(user);
                    var authenticatedUser = _mapper.Map<AuthenticatedUserModel>(user);

                    var userRole = await _repo.GetUserRoleByUserID(user.Id);
                    var roleString = userRole.ToString();

                    return JwtUtil.CreateJwtToken(_jwtTokenSetting, authenticatedUser, roleString);
                }
                catch (Exception)
                {
                    return null!;
                }
            }
            throw new NonAuthenticateException();
        }
    }
}
