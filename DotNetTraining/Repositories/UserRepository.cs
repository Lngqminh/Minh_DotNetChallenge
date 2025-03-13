using System.Data;
using Common.Databases;
using Common.Repositories;
using Dapper;
using DotNetTraining.Data;
using DotNetTraining.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.Repositories
{
    public class UserRepository(IDbConnection connection): SimpleCrudRepository<User,Guid>(connection)
    {
        public async Task<List<User>> GetAllUsers()
        {
            var sql = SqlCommandHelper.GetSelectSql<User>();
            var result = await connection.QueryAsync<User>(sql);
            return result.ToList();
        }

        public async Task<User?> Create(User user)
        {
            return await CreateAsync(user);
        }
    }
}
