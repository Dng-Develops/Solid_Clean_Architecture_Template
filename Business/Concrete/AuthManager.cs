using Business.Abstract;
using Core.Utilities.Hashing;
using Entities.Dtos;
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
            if(result)
            {
                return "Login is successfully ";
            }
            return "User info is incorrect";
        }

        public void Register(RegisterAuthDto registerDto)
        {
            _userService.Add(registerDto);
        }
    }
}
