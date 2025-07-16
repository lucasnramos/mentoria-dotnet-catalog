using System;
using Catalog.API.Core.Domain.Entities;
using Catalog.API.Core.Services.Application.AppProduct;

namespace Catalog.API.Core.Services.Application.Interfaces;

public interface IProductAppService
{
    Task<IEnumerable<Product>> GetAllAsync();
    Task<Product> GetByIdAsync(Guid id);
    Task<Product> InsertAsync(ProductInput productInput);
    Task<Product> UpdateAsync(Guid id, ProductInput productInput);
    Task DeleteAsync(Guid id);
}
