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
    public class UserService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection, IConfiguration configuration, IOptions<JwtTokenSetting> jwtTokenSetting, ILogger<UserService> logger) : BaseService(services)
    {
        private readonly UserRepository _repo = new(connection);
        private readonly IConfiguration _configuration = configuration;
        private readonly JwtTokenSetting _jwtTokenSetting = jwtTokenSetting.Value;
        private readonly ILogger<UserService> _logger = logger;
        public async Task<BasePaginationList<UserModel>> GetAllUsers(PaginationFilter pagination, SortingFilter sorting, FilteringFilter filtering)
        {
            try
            {
                var users = _repo.GetAllUsersQuery();

                if (!string.IsNullOrEmpty(filtering.SearchTerm))
                {
                    users = users.Where(e => e.FullName.Contains(filtering.SearchTerm));
                }

                if (!string.IsNullOrEmpty(sorting.SortBy))
                {
                    users = sorting.Descending
                        ? users.OrderByDescending(e => e.GetType().GetProperty(sorting.SortBy).GetValue(e))
                        : users.OrderBy(e => e.GetType().GetProperty(sorting.SortBy).GetValue(e));
                }

                var totalCount = users.Count();
                var pagedUsers = users.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                    .Take(pagination.PageSize)
                                    .ToList();

                var result = _mapper.Map<List<UserModel>>(pagedUsers);
                return new BasePaginationList<UserModel>(result, totalCount, pagination.PageNumber, pagination.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách user");
                throw;
            }
        }

        public async Task<UserModel?> GetUserById(Guid id)
        {
            try
            {
                var result = await _repo.GetUserById(id);
                if (result == null)
                    throw new Exception("Not found user");

                return _mapper.Map<UserModel>(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy user theo ID: {Id}", id);
                throw;
            }
        }

        public async Task<User?> CreateUser(UserDto newUser)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo user mới: {Email}", newUser.Email);
                throw;
            }
        }
        public async Task<User?> UpdateUser(UserDto updatedUser, Guid id)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật user: {Id}", id);
                throw;
            }
        }

        public async Task DeleteUser(Guid id)
        {
            try
            {
                var user = await _repo.GetUserById(id);
                await _repo.DeleteAsync(user);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa user: {Id}", id);
                throw;
            }
        }

        public async Task<User?> GetUserByEmail(string email)
        {
            try
            {
                var result = await _repo.GetByEmailAsync(email);
                if (result == null)
                    throw new Exception("User not found");
                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy user theo email: {Email}", email);
                throw;
            }
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
