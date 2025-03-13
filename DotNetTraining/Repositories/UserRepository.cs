using System.Data;
using Common.Repositories;
using DotNetTraining.Data;
using DotNetTraining.Domains.Entities;
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.Repositories
{
    public class UserRepository(IDbConnection connection): SimpleCrudRepository<User,Guid>(connection)
    {
        private readonly ApplicationDbContext _context;

       

        public async Task<IEnumerable<User>> GetAllUsersAsync() => await _context.Users.ToListAsync();

        public async Task<User?> GetUserByIdAsync(int id) => await _context.Users.FindAsync(id);

        public async Task<User> CreateUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public Task GetUserByIdAsync()
        {
            throw new NotImplementedException();
        }
    }
}
