using Bazingo_Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.Validators
{
    public class ShippingValidator : AbstractValidator<Shipping>
    {
        public ShippingValidator( )
        {
            RuleFor(x => x.OrderID)
                .NotEmpty().WithMessage("OrderID is required.");

            RuleFor(x => x.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");

            RuleFor(x => x.City)
                .NotEmpty().WithMessage("City is required.");

            RuleFor(x => x.Country)
                .NotEmpty().WithMessage("Country is required.");

            RuleFor(x => x.PostalCode)
                .NotEmpty().WithMessage("PostalCode is required.")
                .MaximumLength(20).WithMessage("PostalCode cannot exceed 20 characters.");

            RuleFor(x => x.ShippingStatus)
                .IsInEnum().WithMessage("Invalid Shipping Status.");
        }
    }

}
