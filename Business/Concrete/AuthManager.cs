using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Concrete;
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

        public Result Register(RegisterAuthDto registerDto)
        {
            UserValidator userValidator = new UserValidator();
            ValidationTool.Validate(userValidator, registerDto);

            bool isExists = CheckIfEmailExists(registerDto.Email);
            Result result = new Result();

            if (isExists)
            {
                _userService.Add(registerDto);

                result.Success = true;
                result.Message = "Registration successfull";


            }
            else
            {
                result.Success = false;
                result.Message = "This mail already exists";
            }
            return result;
        }

        bool CheckIfEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
            {
                return false;
            }
            return true;
        }
    }
}
