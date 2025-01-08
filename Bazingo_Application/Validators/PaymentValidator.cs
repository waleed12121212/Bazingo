using Bazingo_Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.Validators
{
    public class PaymentValidator : AbstractValidator<Payment>
    {
        public PaymentValidator( )
        {
            RuleFor(x => x.OrderID)
                .NotEmpty().WithMessage("OrderID is required.");

            RuleFor(x => x.PaymentMethod)
                .IsInEnum().WithMessage("Invalid Payment Method.");

            RuleFor(x => x.PaymentAmount)
                .GreaterThan(0).WithMessage("PaymentAmount must be greater than 0.");

            RuleFor(x => x.Status)
                .IsInEnum().WithMessage("Invalid Payment Status.");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("CreatedAt is required.");
        }
    }
}
