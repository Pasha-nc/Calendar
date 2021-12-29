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
            

            MyRecord myRecord1 = new(myUser, DateTime.Now, "firstRecord");
            MyRecord myRecord2 = new(myUser, DateTime.Now, "secondRecord");

            //unitOfWork.RecordRepo.Insert(myRecord1);
            //unitOfWork.RecordRepo.Insert(myRecord2);

            //unitOfWork.Save();

            return View();
        }

        protected override void Dispose(bool disposing)
        {
            unitOfWork.Dispose();
            base.Dispose(disposing);
        }
    }
}
