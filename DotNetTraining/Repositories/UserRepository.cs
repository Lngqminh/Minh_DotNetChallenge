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
        public async Task<IEnumerable<User>> GetAllUser()
        {
            //var sql = "SELECT * FROM Users";
            //var user = await _connection.QueryAsync<User>(sql);
            //return user.ToList();
            return await GetAllAsync();
        }

        public async Task<User?> GetUserById(Guid id)
        {
            //var sql = "SELECT * FROM Users WHERE Id = @Id";
            return await GetByIdAsync(id);
        }

        public async Task<User?> CreateUser(User user)
        {
            //var sql = @"
            //    INSERT INTO Users (Id, Name, Email, Password) 
            //    VALUES (@Id, @Name, @Email, @Password);

            //    SELECT * FROM Users WHERE Id = @Id;"; // Lấy lại user vừa tạo

            //return await _connection.QuerySingleOrDefaultAsync<User>(sql, user);
            return await CreateAsync(user);
        }
        public async Task<User?> UpdateUser(User user)
        {
            //var sql = @"
            //    UPDATE Users SET Name = @Name, Email = @Email, Password = @Password 
            //    WHERE Id = @Id;

            //    SELECT * FROM Users WHERE Id = @Id;"; // Lấy lại user sau khi update

            //return await _connection.QuerySingleOrDefaultAsync<User>(sql, new { user.Name, user.Email, user.Password, Id = id });

            return await UpdateAsync(user);
        }

        public async Task DeleteUser(User user)
        {
            //var sql = "DELETE FROM Users WHERE Id = @Id";
            //await _connection.ExecuteAsync(sql, new {Id = id});
             await DeleteAsync(user);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            var sql = "SELECT * FROM Users WHERE Email = @Email";
            return await _connection.QuerySingleOrDefaultAsync<User>(sql, new { Email = email });
        }

        public async Task<User?> GetUserRoleByUserID(Guid id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            return await _connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        }
    }
}
