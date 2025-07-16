using System;
using FluentValidation;

namespace Catalog.API.Core.Domain.Entities.Validations;

public class ProductValidations : AbstractValidator<Product>
{
    public ProductValidations()
    {
        ValidateProperties();
    }

    private void ValidateProperties()
    {
        RuleFor(p => p.Title)
            .NotEmpty().WithMessage("Title is required.")
            .Length(2, 100).WithMessage("Title must be between 2 and 100 characters.");

        RuleFor(p => p.Price)
            .GreaterThan(0).WithMessage("Price must be greater than zero.");

        RuleFor(p => p.ThumbnailUrl)
            .NotEmpty().WithMessage("Thumbnail URL is required.")
            .Must(BeAValidUrl).WithMessage("Thumbnail URL is not valid.");
    }

    private bool BeAValidUrl(string url)
    {
        return Uri.TryCreate(url, UriKind.Absolute, out _);
    }
}

