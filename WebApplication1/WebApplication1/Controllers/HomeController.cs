using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        MyDbContext db = new();
        public IActionResult Index()
        {
            User myUser = new() { Id = Guid.NewGuid(), Login = "user1", PassHash = "pass", Records = new() };

            db.Users.Add(myUser);
            

            MyRecord myRecord1 = new(myUser, DateTime.Now, "firstRecord",RecordStatus.ToStart);
            MyRecord myRecord2 = new(myUser, DateTime.Now, "secondRecord",RecordStatus.InProgress);
            MyRecord myRecord3 = new(myUser, DateTime.Now, "thirdRecord", RecordStatus.Done);

            MyRecord myRecord4 = new(myUser, new(2022, 1, 2), "someRecord", RecordStatus.InProgress);

            myRecord1.Description = "firstDescription";
            myRecord2.Description = "secondDescription";
            myRecord3.Description = "thirdDescription";

            myRecord4.Description = "someDescription";

            //db.Records.Add(myRecord1);
            //db.Records.Add(myRecord2);
            //db.Records.Add(myRecord3);

            //db.Records.Add(myRecord4);

            //db.SaveChanges();

            return View();
        }
    }
}
