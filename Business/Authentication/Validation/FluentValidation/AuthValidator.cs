using Entities.Concrete;
using Entities.Dtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation
{
    public class AuthValidator : AbstractValidator<RegisterAuthDto>
    {
        public AuthValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Email in not valid");
            RuleFor(p => p.Image).NotEmpty().WithMessage("Image cannot be empty");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Password cannot be empty");
            RuleFor(p => p.Password).MinimumLength(6).WithMessage("Password should be at least 6 character");
            RuleFor(p => p.Password).Matches("[A-Z]").WithMessage("Password should contain at least 1 uppercase letter");
            RuleFor(p => p.Password).Matches("[a-z]").WithMessage("Password should contain at least 1 lowercase letter");
            RuleFor(p => p.Password).Matches("[0-9]").WithMessage("Password should contain at least 1 number");
            RuleFor(p => p.Password).Matches("[^a-zA-Z0-9]").WithMessage("Password should contain at least 1 special character `!,#,.` ");
        }
    }
}
