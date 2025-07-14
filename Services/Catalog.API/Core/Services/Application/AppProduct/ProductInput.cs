using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Core.Services.Application.AppProduct;

public class ProductInput
{
    [Required]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Title can only contain alphanumeric characters and spaces.")]
    [MinLength(3, ErrorMessage = "Title must be at least 3 characters long.")]
    public required string Title { get; set; }

    [Required]
    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    public required decimal Price { get; set; }

    [Required]
    public required string ThumbnailUrl { get; set; }
}
