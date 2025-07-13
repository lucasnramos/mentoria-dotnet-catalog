using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Core.Domain.Entities;

public class Product
{
    public Product(string title, decimal price)
    {
        Id = Guid.NewGuid();
        Title = title;
        Price = price;
    }

    public Guid Id { get; set; }

    [Required]
    [StringLength(100, ErrorMessage = "Title cannot be longer than 100 characters.")]
    public string Title { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
    [Required]
    public decimal Price { get; set; }
}
