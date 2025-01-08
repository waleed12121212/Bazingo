using Bazingo_Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.Validators
{
    public class ComplaintValidator : AbstractValidator<Complaint>
    {
        public ComplaintValidator( )
        {
            RuleFor(x => x.UserID)
                .NotEmpty().WithMessage("UserID is required.");

            RuleFor(x => x.OrderID)
                .NotEmpty().WithMessage("OrderID is required.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(500).WithMessage("Description cannot exceed 500 characters.");
        }
    }
}
