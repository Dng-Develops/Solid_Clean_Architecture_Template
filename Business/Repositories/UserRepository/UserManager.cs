﻿using Business.Utilities.File;
using Core.Utilities.Hashing;
using DataAccess.Repositories.UserRepository;
using Entities.Concrete;
using Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repositories.UserRepository
{
    public class UserManager : IUserService
    {

        public readonly IUserDal _userDal;
        public readonly IFileService _fileService;

        public UserManager(IUserDal userDal, IFileService fileService)
        {
            _userDal = userDal;
            _fileService = fileService;
        }

        public async void Add(RegisterAuthDto registerDto)
        {

            string fileName = _fileService.FileSaveToServer(registerDto.Image, "./Content/Images/");
            //byte[] fileByteArray = _fileService.FileConvertByteArrayToDatabase(registerDto.Image); // Use this if you wanna save images as byte array.
            //string fileToFtp = _fileService.FileSaveToFtp(registerDto.Image); // Use this if you wanna save images using FTP.
            var user = CreateUser(registerDto, fileName);
            _userDal.Add(user);
        }


        private User CreateUser(RegisterAuthDto registerDto, string fileName)
        {
            byte[] passwordHash, passwordSalt;
            HashingHelper.CreatePassword(registerDto.Password, out passwordHash, out passwordSalt);
            User user = new User();
            user.Id = 0;
            user.Name = registerDto.Name;
            user.Email = registerDto.Email;
            user.ImageUrl = fileName;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            return user;
        }

        public User GetByEmail(string email)
        {
            var result = _userDal.Get(i => i.Email == email);
            return result;
        }

        public List<User> GetList()
        {
            return _userDal.GetAll();
        }
    }
}