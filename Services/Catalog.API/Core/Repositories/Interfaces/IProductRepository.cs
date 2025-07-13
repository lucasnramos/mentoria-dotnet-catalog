using System;
using Catalog.API.Core.Domain.Entities;

namespace Catalog.API.Core.Repositories.Interfaces;

public interface IProductRepository
{
    // Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task AddAsync(Product product);
    Task UpdateAsync(Product product);
    Task DeleteAsync(Guid id);
}
