using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class CalendarController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
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

        [HttpPost]
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
