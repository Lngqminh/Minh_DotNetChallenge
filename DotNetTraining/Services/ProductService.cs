using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;
using System.Data;

namespace DotNetTraining.Services
{
    [ScopedService]
    public class ProductService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly ProductRepository _repo = new(connection);
        public async Task<IEnumerable<ProductDto>> GetAllProducts()
        {
            var products = await _repo.GetAllAsync();
            var result = _mapper.Map<IEnumerable<ProductDto>>(products);
            return result;
        }
        public async Task<Product?> GetProductById(Guid id)
        {
            var result = await _repo.GetByIdAsync(id);
            if (result == null)
                throw new Exception("Not found product");
            return result;
        }
        public async Task<Product?> CreateProduct(ProductDto newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);
            product.Id = Guid.NewGuid();
            var result = await _repo.CreateAsync(product);
            if (result == null)
                throw new Exception("Can not create new product");
            return result;
        }
        public async Task<Product?> UpdateProduct(ProductDto updatedProduct, Guid id)
        {
            var exitedProduct = await _repo.GetById(id);
            var product = _mapper.Map(updatedProduct, exitedProduct);
            var result = await _repo.UpdateAsync(product);
            if (result == null)
                throw new Exception("Can Not update product");
            return result;
        }
        public async Task DeleteProduct(Guid id)
        {
            var product = await _repo.GetByIdAsync(id);
            await _repo.Delete(product);
        }
    }
}
