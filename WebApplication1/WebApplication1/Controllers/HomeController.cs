using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            //using MyDbContext my = new();

            //User myUser1 = new() { Id = Guid.NewGuid(), Login = "Login1" };
            //User myUser2 = new() { Id = Guid.NewGuid(), Login = "Login2" };

            //my.Users.AddRange(myUser1, myUser2);

            //my.SaveChanges();

            return View();
        }
    }
}
