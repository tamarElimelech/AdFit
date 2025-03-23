using AdFit.API.Models;
using AdFit.Core.Model;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdFit.Core.Repositories
{
    public interface IUserRepository
    {
        List<User> GetUsers();
        User AddUser(User user);
        User UpdateUser(int id,User user);
        void DeleteUser(int id);

        User GetUserById(int id);

        User GetUserByEmail(string email);

        User SignUp(User user);
    }
}
