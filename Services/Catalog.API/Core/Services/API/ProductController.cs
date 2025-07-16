using Catalog.API.Core.Services.Application.AppProduct;
using Catalog.API.Core.Services.Application.Interfaces;
using Marraia.Notifications.Base;
using Marraia.Notifications.Models;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

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

        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            var product = await _productAppService.GetAllAsync();
            return OkOrNotFound(product);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync([FromRoute] Guid id)
        {
            var product = await _productAppService.GetByIdAsync(id);
            return OkOrNotFound(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> InsertAsync([FromBody] ProductInput productInput)
        {
            var product = await _productAppService.InsertAsync(productInput);
            return CreatedContent($"api/product/{product.Id}", product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] Guid id, [FromBody] ProductInput productInput)
        {
            var updatedProduct = await _productAppService.UpdateAsync(id, productInput);
            return AcceptedOrContent(updatedProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] Guid id)
        {
            await _productAppService.DeleteAsync(id);
            return NoContent();
        }
    }
}
