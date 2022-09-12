using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Utilities.Hashing;
using Entities.Dtos;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class AuthManager : IAuthService
    {
        private readonly IUserService _userService;

        public AuthManager(IUserService userService)
        {
            _userService = userService;
        }

        public string Login(LoginAuthDto loginDto)
        {
            var user = _userService.GetByEmail(loginDto.Email);
            var result = HashingHelper.VerifyPasswordHash(loginDto.Password, user.PasswordHash, user.PasswordSalt);
            if (result)
            {
                return "Login is successfully ";
            }
            return "User info is incorrect";
        }

        public List<string> Register(RegisterAuthDto registerDto)
        {
            UserValidator userValidator = new UserValidator();
            ValidationResult result = userValidator.Validate(registerDto);
            List<string> resultList = new List<string>();
            if (result.IsValid)
            {
                _userService.Add(registerDto);
                resultList.Add("Registration complete");
                return resultList;
            }
            resultList = result.Errors.Select(p => p.ErrorMessage).ToList();
            return resultList;
        }
    }
}
