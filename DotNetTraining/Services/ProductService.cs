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
    public class ProductService(IServiceProvider services, ApplicationSetting setting, IDbConnection connection, ILogger<ProductService> logger) : BaseService(services)
    {
        private readonly ProductRepository _repo = new(connection);
        private readonly ILogger<ProductService> _logger = logger;
        public async Task<BasePaginationList<ProductModel>> GetAllProducts(PaginationFilter pagination, SortingFilter sorting, FilteringFilter filtering)
        {
            try
            {
                var products = await _repo.GetAllAsync();

                if (!string.IsNullOrEmpty(filtering.SearchTerm))
                {
                    products = products.Where(e => e.Name.Contains(filtering.SearchTerm));
                }

                if (!string.IsNullOrEmpty(sorting.SortBy))
                {
                    products = sorting.Descending
                        ? products.OrderByDescending(e => e.GetType().GetProperty(sorting.SortBy)?.GetValue(e))
                        : products.OrderBy(e => e.GetType().GetProperty(sorting.SortBy)?.GetValue(e));
                }

                var totalCount = products.Count();
                var pagedProduct = products.Skip((pagination.PageNumber - 1) * pagination.PageSize)
                                           .Take(pagination.PageSize)
                                           .ToList();

                var result = _mapper.Map<List<ProductModel>>(pagedProduct);
                return new BasePaginationList<ProductModel>(result, totalCount, pagination.PageNumber, pagination.PageSize);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy danh sách sản phẩm");
                throw;
            }
        }
        public async Task<Product?> GetProductById(Guid id)
        {
            try
            {
                var result = await _repo.GetByIdAsync(id);
                if (result == null)
                    throw new Exception("Không tìm thấy sản phẩm");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi lấy sản phẩm theo ID: {Id}", id);
                throw;
            }
        }
        public async Task<Product?> CreateProduct(ProductDto newProduct)
        {
            try
            {
                var product = _mapper.Map<Product>(newProduct);
                product.Id = Guid.NewGuid();

                var result = await _repo.CreateAsync(product);
                if (result == null)
                    throw new Exception("Không thể tạo sản phẩm mới");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi tạo sản phẩm: {Name}", newProduct.Name);
                throw;
            }
        }
        public async Task<Product?> UpdateProduct(ProductDto updatedProduct, Guid id)
        {
            try
            {
                var exitedProduct = await _repo.GetById(id);
                var product = _mapper.Map(updatedProduct, exitedProduct);

                var result = await _repo.UpdateAsync(product);
                if (result == null)
                    throw new Exception("Không thể cập nhật sản phẩm");

                return result;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi cập nhật sản phẩm: {Id}", id);
                throw;
            }
        }
        public async Task DeleteProduct(Guid id)
        {
            try
            {
                var product = await _repo.GetByIdAsync(id);
                if (product == null)
                    throw new Exception("Sản phẩm không tồn tại");

                await _repo.Delete(product);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Lỗi khi xóa sản phẩm: {Id}", id);
                throw;
            }
        }
    }
}
