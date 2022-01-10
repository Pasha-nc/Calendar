using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;
using WebApplication1.DAL;

namespace WebApplication1.Controllers
{
    public class CalendarController : Controller
    {
        UnitOfWork unitOfWork;
        public CalendarController()
        {
            unitOfWork = new();
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpDelete]
        public IActionResult DelRec(int recId)
        {
            unitOfWork.RecordRepo.Delete(recId);

            unitOfWork.Save();

            return Ok();
        }

        [HttpPost]
        public IActionResult AddRec(string myDate, string title, int status)
        {
            bool correctInput = DateTime.TryParse(myDate, out DateTime myDateTime);

            User myUser = unitOfWork.UserRepo.Get().FirstOrDefault();

            MyRecord myRecord = null;

            if (correctInput)
            {
                myRecord = new(myUser, myDateTime, title, (RecordStatus)status);
            }
            else 
            {
                return BadRequest();
            }

            if (myRecord != null)
            {
                unitOfWork.RecordRepo.Insert(myRecord);

                unitOfWork.Save();                
            }            

            return Ok();
        }

        [HttpGet]
        public IActionResult ChangeMonth(string selectedMonth, int offset)
        {
            string[] x = new string[2];

            int month = DateTime.Now.Month, year = DateTime.Now.Year;

            if (selectedMonth != null && selectedMonth != string.Empty)
            {
                x = selectedMonth.Split('.');

                bool correctInput = int.TryParse(x[0], out month);

                if (correctInput)
                {
                    correctInput = int.TryParse(x[1], out year);
                }

                if (!correctInput)
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
            }
            return Json(CalendarDataProvider.Provide(year, month, offset));
        }

        [HttpGet]
        public IActionResult GetCalendarData(string selectedMonth)
        {
            string[] x = new string[2];

            int month = DateTime.Now.Month, year = DateTime.Now.Year;

            if (selectedMonth != null && selectedMonth != string.Empty)
            {
                x = selectedMonth.Split('.');

                bool correctInput = int.TryParse(x[0], out month);

                if (correctInput)
                {
                    correctInput = int.TryParse(x[1], out year);
                }

                if (!correctInput)
                {
                    year = DateTime.Now.Year;
                    month = DateTime.Now.Month;
                }
            }

            return Json(CalendarDataProvider.Provide(year, month));
        }

        [HttpGet]
        public IActionResult GetUserRecords(string selDate)
        {
            int year = DateTime.Now.Year;
            int month = DateTime.Now.Month;
            int day = DateTime.Now.Day;

            var myDateArray = selDate.Split('.');

            bool correctInput = int.TryParse(myDateArray[0], out day);

            if (correctInput)
            {
                correctInput = int.TryParse(myDateArray[1], out month);
            }

            if (correctInput)
            {
                correctInput = int.TryParse(myDateArray[2], out year);
            }

            DateTime myDate = DateTime.Now;

            if (correctInput)
            {
                try
                {
                    myDate = new(year, month, day);
                }
                catch (ArgumentOutOfRangeException e)
                {
                    Console.WriteLine(e.Message);
                    correctInput = false;
                }
            }

            var records = correctInput ? unitOfWork.RecordRepo.Get(r => r.MyDateTime.Date == myDate).OrderBy(r => r.MyDateTime)
                            .Select(r => new
                            {
                                id = r.Id,
                                myDateTime = r.MyDateTime,
                                title = r.Title,
                                status = r.Status.ToString()
                            }) : null;

            return Json(records);
        }

        [HttpGet]
        public IActionResult GetDescr(string mydate, string recId)
        {
            bool correctInput = int.TryParse(recId, out int id);
            MyRecord record = null;

            if (correctInput)
            {
                record = unitOfWork.RecordRepo.Get(id);
            }

            return Json(new
            {
                id = record.Id,
                myDateTime = record.MyDateTime,
                title = record.Title,
                status = record.Status.ToString(),
                description = record.Description
            });
        }
    }
}
