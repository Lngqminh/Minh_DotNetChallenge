using Common.Repositories;
using DotNetTraining.Domains.Entities;
using System.Data;
using Dapper;
using DotNetTraining.Domains.Entities;  
using Microsoft.EntityFrameworkCore;

namespace DotNetTraining.Repositories
{
    public class ProductRepository(IDbConnection connection): SimpleCrudRepository<Product, Guid>(connection)
    {
        public IEnumerable<Product> GetAll()
        {
            var sql = "SELECT * FROM Product";
            return _connection.Query<Product>(sql); // Trả về IEnumerable<Product>
        }

        public async Task<Product?> GetById(Guid id)
        {
            return await GetByIdAsync(id);
        }

        public async Task<Product?> Create(Product product)
        {
            return await CreateAsync(product);
        }

        public async Task<Product?> Update(Product product)
        {
            return await UpdateAsync(product);
        }

        public async Task Delete(Product product)
        {
             await DeleteAsync(product);
        }


    }
}
