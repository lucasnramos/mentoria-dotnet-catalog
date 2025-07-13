using System;
using Catalog.API.Core.Domain.Entities;

namespace Catalog.API.Core.Services.Application.Interfaces;

public interface IProductAppService
{
    Task<Product> GetProductByIdAsync(Guid id);
    Task AddProductAsync(Product product);
    Task UpdateProductAsync(Product product);
    Task DeleteProductAsync(Guid id);
}
