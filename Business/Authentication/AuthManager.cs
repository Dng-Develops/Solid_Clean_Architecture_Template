using Business.Repositories.UserOperationClaimRepository.Validation.FluentValidation;
using Business.Repositories.UserRepository;
using Core.Aspects.Validation;
using Core.Utilities.Business;
using Core.Utilities.Hashing;
using Core.Utilities.Result.Abstract;
using Core.Utilities.Result.Concrete;
using Entities.Dtos;

namespace Business.Authentication
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

            string fileName = registerDto.Image.FileName;

            IResult result = BusinessRules.Run(
                CheckIfEmailExists(registerDto.Email),
                 CheckIfImgExtensionAllowed(fileName),
            CheckIfImgLessThanOneMb(registerDto.Image.Length)
                );

            if (result != null)
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

        private IResult CheckIfImgLessThanOneMb(long imgSize)
        {
            decimal imgMbSize = Convert.ToDecimal(imgSize * 0.000001);
            if (imgMbSize > 1)
            {
                return new ErrorResult("Image too large! image size cannot be more than 1mb");
            }
            return new SuccessResult();
        }

        private IResult CheckIfImgExtensionAllowed(string fileName)
        {
            var extension = fileName.Substring(fileName.LastIndexOf(".")).ToLower();
            List<string> AllowFileExtensions = new List<string> { ".jpg", ".jpeg", ".gif", ".png" };
            if (!AllowFileExtensions.Contains(extension))
            {
                return new ErrorResult("Image file extension should be one of .jpg, .jpeg, .gif, .png");
            }
            return new SuccessResult();
        }
    }
}
