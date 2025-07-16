using System;
using System.ComponentModel.DataAnnotations;
using Catalog.API.Core.Domain.Entities.Validations;
using Marraia.Notifications.Validations;

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

    public FieldValidation Validate()
    {
        var fieldValidation = new FieldValidation(true);

        var valid = new ProductValidations().Validate(this);

        if (valid.IsValid) return fieldValidation;

        fieldValidation.AssignValid(valid.IsValid);
        fieldValidation.AddValidation(valid);

        return fieldValidation;
    }
}
