using System;
using System.ComponentModel.DataAnnotations;

namespace Catalog.API.Core.Domain.Entities;

public class Product
{
    public Product(string title, decimal price, string thumbnailUrl)
    {
        Id = Guid.NewGuid();
        Title = title;
        Price = price;
        ThumbnailUrl = thumbnailUrl;
    }

    public Guid Id { get; set; }

    public string Title { get; set; }

    public decimal Price { get; set; }

    public string ThumbnailUrl { get; set; }
}
