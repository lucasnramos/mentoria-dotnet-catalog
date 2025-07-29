using System;
using System.Text.Json;
using Catalog.API.Core.Domain.Entities;
using Catalog.API.Core.Repositories.Interfaces;
using Microsoft.Extensions.Caching.Distributed;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;

namespace Catalog.API.Core.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly IDistributedCache _cache;
    private readonly IConnectionMultiplexer _redis;

    public ProductRepository(IDistributedCache cache, IConnectionMultiplexer redis)
    {
        _cache = cache ?? throw new ArgumentNullException(nameof(cache));
        _redis = redis ?? throw new ArgumentNullException(nameof(redis));
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

    public async Task<Product?> GetByIdAsync(Guid id)
    {
        var serialized = await _cache.GetStringAsync(id.ToString());
        if (string.IsNullOrEmpty(serialized))
        {
            return null;
        }

        var product = JsonSerializer.Deserialize<Product>(serialized);

        if (product == null)
        {
            return null;
        }
        return product!;
    }

    public async Task UpdateAsync(Product product)
    {
        var serialized = JsonSerializer.Serialize(product);
        await _cache.SetStringAsync(product.Id.ToString(), serialized);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        var server = _redis.GetServer(_redis.GetEndPoints().First());
        var keys = server.Keys(pattern: "*").ToArray();

        var products = new List<Product>();
        foreach (var key in keys)
        {
            var id = key.ToString().Split("CatalogAPI")[1];
            var serialized = await _cache.GetStringAsync(id);
            if (!string.IsNullOrEmpty(serialized))
            {
                var product = JsonSerializer.Deserialize<Product>(serialized);
                if (product != null)
                {
                    products.Add(product);
                }
            }
        }

        return products;
    }
}

