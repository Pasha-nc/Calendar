using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;

namespace WebApplication1.Models
{
    public static class UserHandler
    {
        public static User GetUser(User myUser)
        {
            using MyDbContext my = new();

            return my.Users.FirstOrDefault(o => o == myUser);
        }

        public static User GetUserByLogin(string login)
        {
            using MyDbContext my = new();

            return my.Users.FirstOrDefault(o => o.Login == login);
        }
    }
}
