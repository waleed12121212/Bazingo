using Bazingo_Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bazingo_Core.Models;

namespace Bazingo_Application.Validators
{
    public class ProductValidator : AbstractValidator<Product>
    {
        public ProductValidator( )
        {
            RuleFor(x => x.ProductName)
                .NotEmpty().WithMessage("Product Name is required.")
                .MaximumLength(150).WithMessage("Product Name cannot exceed 150 characters.");

            RuleFor(x => x.Description)
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than 0.");

            RuleFor(x => x.Quantity)
                .GreaterThanOrEqualTo(0).WithMessage("Quantity cannot be negative.");

            RuleFor(x => x.CategoryID)
                .NotEmpty().WithMessage("CategoryID is required.");

            RuleFor(x => x.BaseCurrencyID)
                .NotEmpty().WithMessage("BaseCurrencyID is required.");
        }
    }
}
