using Application.Settings;
using Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Repositories;
using System.Data;

namespace DotNetTraining.Services
{
    public class ProductService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly ProductRepository _repo = new(connection);
        public async Task<List<ProductDto>> GetAllProducts()
        {
            var products = await _repo.GetAll();
            var result = _mapper.Map<List<ProductDto>>(products);
            return result;
        }
        public async Task<Product?> GetProductById(Guid id)
        {
            var result = await _repo.GetById(id);
            if (result == null)
                throw new Exception("Not found product");
            return result;
        }
        public async Task<Product?> CreateProduct(ProductDto newProduct)
        {
            var product = _mapper.Map<Product>(newProduct);
            product.Id = Guid.NewGuid();
            var result = await _repo.Create(product);
            if (result == null)
                throw new Exception("Can not create new product");
            return result;
        }
        public async Task<Product?> UpdateProduct(ProductDto updatedProduct, Guid id)
        {
            var product = _mapper.Map<Product>(updatedProduct);
            var result = await _repo.Update(product, id);
            if (result == null)
                throw new Exception("Can Not update product");
            return result;
        }
        public async Task DeleteProduct(Guid id)
        {
            var product = await _repo.GetById(id);
            await _repo.Delete(id);
        }   
    }
}
