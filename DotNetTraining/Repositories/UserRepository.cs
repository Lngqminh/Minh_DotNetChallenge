using System.Data;
using Common.Databases;
using Common.Repositories;
using Dapper;
using DotNetTraining.Data;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.Repositories
{
    public class UserRepository(IDbConnection connection): SimpleCrudRepository<User,Guid>(connection)
    {
        public async Task<List<User>> GetAll()
        {
            var sql = "SELECT * FROM Users";
            var user = await _connection.QueryAsync<User>(sql);
            return user.ToList();
        }

        public async Task<User?> GetById(Guid id)
        {
            var sql = "SELECT * FROM Users WHERE Id = @Id";
            return await _connection.QuerySingleOrDefaultAsync<User>(sql, new { Id = id });
        }

        public async Task<User?> Create(User user)
        {
            var sql = @"
                INSERT INTO Users (Id, Name, Email, Password) 
                VALUES (@Id, @Name, @Email, @Password);
        
                SELECT * FROM Users WHERE Id = @Id;"; // Lấy lại user vừa tạo

            return await _connection.QuerySingleOrDefaultAsync<User>(sql, user);
        }
        public async Task<User?> Update(User user, Guid id)
        {
            var sql = @"
                UPDATE Users SET Name = @Name, Email = @Email, Password = @Password 
                WHERE Id = @Id;

                SELECT * FROM Users WHERE Id = @Id;"; // Lấy lại user sau khi update

            return await _connection.QuerySingleOrDefaultAsync<User>(sql, new { user.Name, user.Email, user.Password, Id = id });
        }

        public async Task Delete(Guid id)
        {
            var sql = "DELETE FROM Users WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new {Id = id});
        }   
    }
}
