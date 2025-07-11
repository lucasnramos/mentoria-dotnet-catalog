using System;

namespace Catalog.API.Core.Repositories.Interfaces;

public interface IProductRepository
{
    // Task<IEnumerable<Product>> GetAllAsync();
    // Task<Product> GetByIdAsync(Guid id);
    Task AddAsync();
    // Task UpdateAsync(Product product);
    // Task DeleteAsync(Guid id);
}
