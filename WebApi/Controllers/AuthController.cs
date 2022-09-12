﻿using Business.Abstract;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]

        public IActionResult Register(RegisterAuthDto authDto)
        {
           var result = _authService.Register(authDto);
            if (result.Success)
            {
             return Ok(result);
            }
            return BadRequest(result.Message);
        }

        [HttpPost("login")]

        public IActionResult login(LoginAuthDto loginDto)
        {
           var result = _authService.Login(loginDto);
            return Ok(result);
        }
    }
}
