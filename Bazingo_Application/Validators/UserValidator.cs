using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bazingo_Core.Models;

namespace Bazingo_Application.Validators
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator( )
        {
            RuleFor(x => x.FirstName)
                .NotEmpty().WithMessage("First Name is required.")
                .MaximumLength(50).WithMessage("First Name cannot exceed 50 characters.");

            RuleFor(x => x.LastName)
                .NotEmpty().WithMessage("Last Name is required.")
                .MaximumLength(50).WithMessage("Last Name cannot exceed 50 characters.");

            RuleFor(x => x.UserType)
                .IsInEnum().WithMessage("Invalid UserType value.");

            RuleFor(x => x.PreferredCurrencyID)
                .GreaterThan(0).WithMessage("PreferredCurrencyID must be valid.");

            RuleFor(x => x.CreatedAt)
                .NotEmpty().WithMessage("CreatedAt is required.");
        }
    }
}
