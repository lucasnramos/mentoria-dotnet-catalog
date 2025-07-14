using Catalog.API.Core.Domain.Entities;
using Catalog.API.Core.Services.Application.AppProduct;
using Catalog.API.Core.Services.Application.Interfaces;
using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Core.Services.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : BaseController
    {
        private readonly IProductAppService _productAppService;

        public ProductController(IProductAppService productAppService,
                                INotificationHandler<DomainNotification> notification)
            : base(notification)
        {

            _productAppService = productAppService ?? throw new ArgumentNullException(nameof(productAppService));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductByIdAsync(Guid id)
        {
            var product = await _productAppService.GetProductByIdAsync(id);
            return OkOrNotFound(product);
        }

        [HttpPost]
        public async Task<IActionResult> InsertProductAsync([FromBody] ProductInput productInput)
        {
            var product = new Product(productInput.Title, productInput.Price, productInput.ThumbnailUrl);
            await _productAppService.InsertProductAsync(product);
            return CreatedContent($"api/product/{product.Id}", product);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProductAsync([FromBody] ProductInput productInput)
        {
            var product = new Product(productInput.Title, productInput.Price, productInput.ThumbnailUrl);
            await _productAppService.UpdateProductAsync(product);
            return AcceptedOrContent(product);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProductAsync([FromQuery] Guid id)
        {
            await _productAppService.DeleteProductAsync(id);
            return NoContent();
        }
    }
}
