using Application.Settings;
using Common.Application.CustomAttributes;
using Common.Services;
using DocumentFormat.OpenXml.Spreadsheet;
using DotNetTraining.Common.Services;
using DotNetTraining.Domains.Dtos;
using DotNetTraining.Domains.Entities;
using DotNetTraining.Domains.Models;
using DotNetTraining.Repositories;
using DotNetTraining.Requests;
using System.Data;

namespace DotNetTraining.Services
{
    [ScopedService]
    public class ProductService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection) : BaseService(services)
    {
        private readonly ProductRepository _repo = new(connection);
        public async Task<BasePaginationList<ProductModel>> GetAllProducts(PaginationFilter pagination, SortingFilter sorting, FilteringFilter filtering)
        {
            var products = await _repo.GetAllAsync();
            //var result = _mapper.Map<IEnumerable<ProductDto>>(products);
            //return result;
            // Áp dụng lọc
            if (!string.IsNullOrEmpty(filtering.SearchTerm))
            {
                products = products.Where(e => e.Name.Contains(filtering.SearchTerm)); // Lọc người dùng theo tên
            }

            // Áp dụng sắp xếp
            if (!string.IsNullOrEmpty(sorting.SortBy))
            {
                products = sorting.Descending
                    ? products.OrderByDescending(e => e.GetType().GetProperty(sorting.SortBy).GetValue(e))
                    : products.OrderBy(e => e.GetType().GetProperty(sorting.SortBy).GetValue(e));
            }

            // Áp dụng phân trang
            var totalCount = products.Count(); // Tổng số người dùng
            var pagedProduct = products.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                   .Take(pagination.PageSize)
                                   .ToList(); // Phân trang và chuyển đổi sang danh sách

            var result = _mapper.Map<List<ProductModel>>(pagedProduct);
            return new BasePaginationList<ProductModel>(result, totalCount, pagination.PageNumber, pagination.PageSize);

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
