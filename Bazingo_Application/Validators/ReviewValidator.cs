using Bazingo_Core.Models;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bazingo_Application.Validators
{
    public class ReviewValidator : AbstractValidator<Review>
    {
        public ReviewValidator( )
        {
            RuleFor(x => x.ProductID)
                .NotEmpty().WithMessage("ProductID is required.");

            RuleFor(x => x.UserID)
                .NotEmpty().WithMessage("UserID is required.");

            RuleFor(x => x.Rating)
                .InclusiveBetween(1 , 5).WithMessage("Rating must be between 1 and 5.");

            RuleFor(x => x.Comment)
                .MaximumLength(500).WithMessage("Comment cannot exceed 500 characters.");
        }
    }

}
