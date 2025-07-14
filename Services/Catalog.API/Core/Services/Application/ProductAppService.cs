using System;
using Catalog.API.Core.Domain.Entities;
using Catalog.API.Core.Repositories.Interfaces;
using Catalog.API.Core.Services.Application.Interfaces;
using Marraia.Notifications.Interfaces;

namespace Catalog.API.Core.Services.Application;

public class ProductAppService : IProductAppService
{
    private readonly IProductRepository _productRepository;
    private readonly ISmartNotification _notification;

    public ProductAppService(IProductRepository productRepository, ISmartNotification notification)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
    }

    public async Task InsertProductAsync(Product product)
    {
        await _productRepository.AddAsync(product);
    }

    public async Task DeleteProductAsync(Guid id)
    {
        await _productRepository.DeleteAsync(id);
    }

    public async Task<Product> GetProductByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            _notification.NewNotificationError("Product not found.");
            return null;
        }
        return product;
    }

    public async Task UpdateProductAsync(Product product)
    {
        await _productRepository.UpdateAsync(product);
    }
}
