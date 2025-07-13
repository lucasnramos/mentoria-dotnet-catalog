using System;
using Catalog.API.Core.Domain.Entities;
using Catalog.API.Core.Repositories.Interfaces;
using Catalog.API.Core.Services.Application.Interfaces;

namespace Catalog.API.Core.Services.Application;

public class ProductAppService : IProductAppService
{
    private readonly IProductRepository _productRepository;
    public ProductAppService(IProductRepository productRepository)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
    }

    public Task AddProductAsync(Product product)
    {
        throw new NotImplementedException();
    }

    public Task DeleteProductAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Product> GetProductByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateProductAsync(Product product)
    {
        throw new NotImplementedException();
    }
}
