using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
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

        [ValidationAspect(typeof(UserValidator))]
        public IResult Register(RegisterAuthDto registerDto)
        {
            int imgSize = 2;
            //UserValidator userValidator = new UserValidator();
            //ValidationTool.Validate(userValidator, registerDto);

            IResult result = BusinessRules.Run(
                CheckIfEmailExists(registerDto.Email),
                CheckIfImgLessThanOneMb(imgSize)
                );

            if (!result.Success)
            {
                return result;
            }

            _userService.Add(registerDto);
            return new SuccessResult("Registration successfull");


        }

        private IResult CheckIfEmailExists(string email)
        {
            var list = _userService.GetByEmail(email);
            if (list != null)
            {
                return new ErrorResult("This mail already exists");
            }
            return new SuccessResult();
        }

        private IResult CheckIfImgLessThanOneMb(int imgSize)
        {
            if (imgSize > 1)
            {
                return new ErrorResult("Image too large! image size cannot be more than 1mb");
            }
            return new SuccessResult();
        }
    }
}
