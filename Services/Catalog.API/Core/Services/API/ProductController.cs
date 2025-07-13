using Catalog.API.Core.Domain.Entities;
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
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IConfiguration _configuration;

        public ProductController(IProductAppService productAppService,
                                INotificationHandler<DomainNotification> notification,
                                IHttpContextAccessor httpContextAccessor,
                                IConfiguration configuration)
            : base(notification)
        {

            _productAppService = productAppService ?? throw new ArgumentNullException(nameof(productAppService));
            _httpContextAccessor = httpContextAccessor ?? throw new ArgumentNullException(nameof(httpContextAccessor));
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var product = await _productAppService.GetProductByIdAsync(id);
            return OkOrNotFound(product);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromBody] Product product)
        {
            // Validate with ProductInput
            await _productAppService.AddProductAsync(product);
            return CreatedContent($"api/product/{product.Id}", product);
        }
    }
}
