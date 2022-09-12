using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IAuthService
    {
        List<string> Register(RegisterAuthDto registerDto);
        string Login(LoginAuthDto loginDto);
    }
}
