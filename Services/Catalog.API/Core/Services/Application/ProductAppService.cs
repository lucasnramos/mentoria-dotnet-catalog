using System;
using Catalog.API.Core.Domain.Entities;
using Catalog.API.Core.Repositories.Interfaces;
using Catalog.API.Core.Services.Application.AppProduct;
using Catalog.API.Core.Services.Application.Interfaces;
using Marraia.Notifications.Interfaces;
using Marraia.Notifications.Validations;

namespace Catalog.API.Core.Services.Application;

public class ProductAppService : EntityValidator, IProductAppService
{
    private readonly IProductRepository _productRepository;
    private readonly ISmartNotification _notification;

    public ProductAppService(IProductRepository productRepository, ISmartNotification notification) : base(notification)
    {
        _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
        _notification = notification ?? throw new ArgumentNullException(nameof(notification));
    }

    public async Task<Product> InsertAsync(ProductInput productInput)
    {
        var product = new Product(productInput.Title, productInput.Price, productInput.ThumbnailUrl);
        var validationFields = product.Validate();
        if (!validationFields.IsValid)
        {
            NotifyValidationErrors(validationFields);
            return null;
        }
        await _productRepository.AddAsync(product);
        return product;
    }

    public async Task DeleteAsync(Guid id)
    {
        if (id == Guid.Empty)
        {
            _notification.NewNotificationBadRequest("Id is required.");
            return;
        }
        var product = await _productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return;
        }
        await _productRepository.DeleteAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _productRepository.GetAllAsync();
    }

    public async Task<Product> GetByIdAsync(Guid id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        return product;
    }

    public async Task<Product> UpdateAsync(Guid id, ProductInput productInput)
    {
        if (id == Guid.Empty)
        {
            _notification.NewNotificationBadRequest("Id is required.");
            return null;
        }

        var existingProduct = await _productRepository.GetByIdAsync(id);

        if (existingProduct == null)
        {
            return null;
        }

        existingProduct.Update(productInput.Title, productInput.Price, productInput.ThumbnailUrl);
        var validationFields = existingProduct.Validate();
        if (!validationFields.IsValid)
        {
            NotifyValidationErrors(validationFields);
            return null;
        }
        await _productRepository.UpdateAsync(existingProduct);
        return existingProduct;
    }
}
