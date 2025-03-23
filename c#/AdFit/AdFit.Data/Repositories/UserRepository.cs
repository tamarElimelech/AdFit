using AdFit.API.Models;
using AdFit.Core.Model;
using AdFit.Core.Repositories;
using AdFit.Core.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Data.Repositories
{
    public class UserRepository:IUserRepository
    {
     private readonly DataContext _context;
     
        public UserRepository(DataContext context)
        {
           _context = context;
        }



        public List<User> GetUsers()
        {
            return _context.Users.Include(u=>u.Advertisements).ToList();
        }

        public User GetUserById(int id)
        {
            return _context.Users.Include(_u => _u.Advertisements).FirstOrDefault(u => u.Id == id);
        }

        public User GetUserByEmail(string email)
        {
            List<User> users = GetUsers();
            User user = users.Find(u => u.Email==email);
            return user;
        }
        public User AddUser(User u)
        {
            _context.Users.Add(u);
            _context.SaveChanges();
            return u;
        }
        public User UpdateUser(int id,User user)
        {
            User newUser=new User();
            foreach(User u in _context.Users.ToList()){
                if (id == u.Id)
                {
                 if(user.Name!=null)
                        u.Name = user.Name;
                 if(user.Email!=null)
                        u.Email = user.Email;
                 if(u.Password!=null)
                        u.Password=user.Password;
                 if(u.Role!=null)
                        u.Role = user.Role;
                    newUser = u;
                    break;
                }
            }
            _context.SaveChanges();
            return newUser;

        }
        public void DeleteUser(int id)
        {
            List<User> users = _context.Users.ToList();
            foreach(User u in users)
            {
                if (u.Id == id)
                {
                 _context.Users.Remove(u);
                    break;
                }
                   
            }
            _context.SaveChanges();      
        }

        public User SignUp(User user)
        {
            
            User u = _context.Users.FirstOrDefault(u=>u.Email==user.Email);
            if (u == null)
            {
                user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);//הצפנה
                _context.Users.Add(user);
                _context.SaveChanges();
                return user;
            }
            else
                return null;
        }

    }
}
