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
    }
}
