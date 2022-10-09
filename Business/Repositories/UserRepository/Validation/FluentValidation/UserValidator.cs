using Entities.Concrete;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository.Validation.FluentValidation
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Name cannot be empty");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Email cannot be empty");
            RuleFor(p => p.Email).EmailAddress().WithMessage("Email in not valid");
            RuleFor(p => p.ImageUrl).NotEmpty().WithMessage("Image cannot be empty");
         
        }
    }
}
