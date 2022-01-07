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
            

            MyRecord myRecord1 = new(myUser, DateTime.Now, "firstRecord",RecordStatus.ToStart);
            MyRecord myRecord2 = new(myUser, DateTime.Now, "secondRecord",RecordStatus.InProgress);
            MyRecord myRecord3 = new(myUser, DateTime.Now, "thirdRecord", RecordStatus.Done);

            MyRecord myRecord4 = new(myUser, new(2022, 1, 2), "someRecord", RecordStatus.InProgress);

            myRecord1.Description = "firstDescription";
            myRecord2.Description = "secondDescription";
            myRecord3.Description = "thirdDescription";

            myRecord4.Description = "someDescription";

            //unitOfWork.RecordRepo.Insert(myRecord1);
            //unitOfWork.RecordRepo.Insert(myRecord2);
            //unitOfWork.RecordRepo.Insert(myRecord3);

            //unitOfWork.RecordRepo.Insert(myRecord4);

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
