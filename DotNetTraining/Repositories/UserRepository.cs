using System.Data;
using Common.Databases;
using Common.Repositories;
using Dapper;
using Dapper.Contrib.Extensions;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.Repositories
{
    public class UserRepository(IDbConnection connection): SimpleCrudRepository<User,Guid>(connection)
    {
        //public async Task<IEnumerable<User>> GetAllUser()
        //{
        //    return await GetAllAsync();
        //}

        public IEnumerable<User> GetAllUsersQuery()
        {
            var sql = "SELECT * FROM Users";
            return _connection.Query<User>(sql); // Trả về IEnumerable<User>
        }

        public async Task<User?> GetUserById(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<User?> CreateUser(User user)
        {
            return await CreateAsync(user);
        }
        public async Task<User?> UpdateUser(User user)
        {
            return await UpdateAsync(user);
        }

        public async Task DeleteUser(User user)
        {
             await DeleteAsync(user);
        }

        //Use for Authentication
        public async Task<User?> GetByUserNameAsync(string username)
        {
            var sql = "SELECT * FROM Users WHERE UserName = @Username";
            return await _connection.QuerySingleOrDefaultAsync<User>(sql, new { Username = username });
        }

        public async Task<string> GetUserRoleByEmail(string email)
        {
            var sql = "SELECT DISTINCT Roles FROM Users WHERE Email = @Email";
            return await _connection.QuerySingleOrDefaultAsync<string>(sql, new { Email = email });
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            return await _connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        }
       
    }
}
