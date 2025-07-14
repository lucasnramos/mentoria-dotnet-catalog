using System;
using System.Text.Json;
using Catalog.API.Core.Domain.Entities;
using Catalog.API.Core.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace Catalog.API.Core.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDistributedCache _cache;

    public ProductRepository(IDistributedCache cache)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
    }

    public async Task AddAsync(Product product)
    {
        var serialized = JsonSerializer.Serialize(product);
        await _cache.SetStringAsync(product.Id.ToString(), serialized);
    }

    public async Task DeleteAsync(Guid id)
    {
        await _cache.RemoveAsync(id.ToString());
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var serialized = await _cache.GetStringAsync(id.ToString());
        if (string.IsNullOrEmpty(serialized))
        {
            return null;
        }
        var product = JsonSerializer.Deserialize<Product>(serialized);
        return product;
    }

    public async Task UpdateAsync(Product product)
    {
        var serialized = JsonSerializer.Serialize(product);
        await _cache.SetStringAsync(product.Id.ToString(), serialized);
    }
}

