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
using Microsoft.Extensions.Options;
using DotNetTraining.Common.Services;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.Services
{
    [ScopedService]
    public class UserService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection, IConfiguration configuration, IOptions<JwtTokenSetting> jwtTokenSetting) : BaseService(services)
    {
        private readonly UserRepository _repo = new(connection);
        private readonly IConfiguration _configuration = configuration;
        private readonly JwtTokenSetting _jwtTokenSetting = jwtTokenSetting.Value;
        public async Task<BasePaginationList<UserModel>> GetAllUsers(PaginationFilter pagination, SortingFilter sorting, FilteringFilter filtering)
        {
            var users = _repo.GetAllUsersQuery();
            // Áp dụng lọc
            if (!string.IsNullOrEmpty(filtering.SearchTerm))
            {
                users = users.Where(e => e.FullName.Contains(filtering.SearchTerm)); // Lọc người dùng theo tên
            }

            // Áp dụng sắp xếp
            if (!string.IsNullOrEmpty(sorting.SortBy))
            {
                users = sorting.Descending
                    ? users.OrderByDescending(e => e.GetType().GetProperty(sorting.SortBy).GetValue(e))
                    : users.OrderBy(e => e.GetType().GetProperty(sorting.SortBy).GetValue(e));
            }

            // Áp dụng phân trang
            var totalCount = users.Count(); // Tổng số người dùng
            var pagedUsers = users.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                   .Take(pagination.PageSize)
                                   .ToList(); // Phân trang và chuyển đổi sang danh sách

            var result = _mapper.Map<List<UserModel>>(pagedUsers);
            return new BasePaginationList<UserModel>(result, totalCount, pagination.PageNumber, pagination.PageSize);
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

            var hasher = new HashingWithKeyService(_configuration);
            user.Password = hasher.HashPassword(updatedUser.Password);

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


        public async Task<(string accessToken, string refreshToken)> AuthenticateAsync(LoginRequest request)
        {
            var user = await _repo.GetByUserNameAsync(request.Username);
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

                    var userRole = await _repo.GetUserRoleByEmail(user.Email);

                    //Tạo JWT Token
                    string jwtToken = JwtUtil.CreateJwtToken(_jwtTokenSetting, authenticatedUser, userRole);
                    //Tạo Refresh Token
                    var refreshToken = Guid.NewGuid().ToString("N");
                    await _repo.SaveRefreshToken(user.Id, refreshToken, DateTime.UtcNow.AddDays(7));
                    string refresh = refreshToken.ToString();

                    return (jwtToken, refresh);
                }
                catch (Exception)
                {
                    return (null,null);
                }
            }
            throw new NonAuthenticateException();
        }

        public async Task<(string accessToken, string refreshToken)> RefreshTokenAsync(RefreshTokenRequest request)
        {
            var tokenEntity = await _repo.GetByToken(request.RefreshToken);
            if (tokenEntity == null || tokenEntity.RefreshToken != request.RefreshToken)
                throw new System.ApplicationException("Refresh Token không hợp lệ hoặc đã hết hạn.");

            var user = await _repo.GetByEmailAsync(tokenEntity.Email);
            if (user == null || user.Status != UserStatus.Active)
                throw new NonAuthenticateException("Tài khoản không hợp lệ hoặc đã bị khóa.");

            var authenticatedUser = _mapper.Map<AuthenticatedUserModel>(user);
            var userRole = await _repo.GetUserRoleByEmail(user.Email);

            var newAccessToken = JwtUtil.CreateJwtToken(_jwtTokenSetting, authenticatedUser, userRole);
            var newRefreshToken = Guid.NewGuid().ToString("N");

            await _repo.UpdateRefreshToken(request.RefreshToken, newRefreshToken);

            return (newAccessToken, newRefreshToken);
        }

        public async Task LogoutAsync(string email)
        {
            await _repo.RemoveRefreshToken(email);
        }
    }
}
