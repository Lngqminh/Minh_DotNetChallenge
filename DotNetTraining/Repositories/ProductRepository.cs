using Common.Repositories;
using Dapper;
using DotNetTraining.Domains.Entities;
using System.Data;

namespace DotNetTraining.Repositories
{
    public class ProductRepository(IDbConnection connection) : SimpleCrudRepository<Product, Guid>(connection)
    {
        public async Task<List<Product>> GetAll()
        {
            var sql = "SELECT * FROM Product";
            var products = await _connection.QueryAsync<Product>(sql);
            return products.ToList();
        }

        public async Task<Product?> GetById(Guid id)
        {
            var sql = "SELECT * FROM Product WHERE Id = @Id";
            return await _connection.QuerySingleOrDefaultAsync<Product>(sql, new { Id = id });
        }

        public async Task<Product?> Create(Product product)
        {
            var sql = @"
                INSERT INTO Product (Id, Name, Description, Price) 
                VALUES (@Id, @Name, @Description, @Price);
        
                SELECT * FROM Product WHERE Id = @Id;"; // Lấy lại product vừa tạo
            return await _connection.QuerySingleOrDefaultAsync<Product>(sql, product);
        }

        public async Task<Product?> Update(Product product, Guid id)
        {
            var sql = @"
                UPDATE Product SET Name = @Name, Description = @Description, Price = @Price 
                WHERE Id = @Id;
                SELECT * FROM Product WHERE Id = @Id;"; // Lấy lại product sau khi update
            return await _connection.QuerySingleOrDefaultAsync<Product>(sql, new { product.Name, product.Description, product.Price, Id = id });
        }

        public async Task Delete(Guid id)
        {
            var sql = "DELETE FROM Product WHERE Id = @Id";
            await _connection.ExecuteAsync(sql, new { Id = id });
        }

    }
}
