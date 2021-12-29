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
        private UnitOfWork unitOfWork;
        public HomeController()
        {
             unitOfWork = new UnitOfWork();
        }
        public IActionResult Index()
        {
            User myUser = new() { Id = Guid.NewGuid(), Login = "user1", PassHash = "pass", Records = new() };

            unitOfWork.UserRepo.Insert(myUser);
            

            MyRecord myRecord = new(myUser, DateTime.Now, "firstRecord");

            unitOfWork.RecordRepo.Insert(myRecord);

            unitOfWork.Save();

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
