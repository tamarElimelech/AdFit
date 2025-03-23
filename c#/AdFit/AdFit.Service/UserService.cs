using AdFit.API.Models;
using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using AdFit.Data.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace AdFit.Service

{
    public class UserService:IUserService
     
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public List<User> GetAll()
        { 
         return _userRepository.GetUsers();
        }

        public User GetUserById(int id) {
            return _userRepository.GetUserById(id);
        }

        public User GetUserByEmail(string email)
        {
            return _userRepository.GetUserByEmail(email);
        }
        public User AddUser(User user)
        {
          return _userRepository.AddUser(user);
        }
        public User UpdateUser(int id, User user)
        {
            return _userRepository.UpdateUser(id, user);
        }
       public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }

        public User SignUp(User user)
        {
            return _userRepository.SignUp(user);
        }

        public bool IsPasswordHashed(string password)
        {
            var bcryptPattern = @"^\$2[ayb]\$.{56}$";

            return Regex.IsMatch(password, bcryptPattern);
        }

    }
}