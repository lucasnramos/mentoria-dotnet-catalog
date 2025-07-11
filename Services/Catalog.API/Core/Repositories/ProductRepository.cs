using System;
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

    public async Task AddAsync()
    {
        await _cache.SetStringAsync("sample_key", "sample_value");
    }
}

