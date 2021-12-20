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
            return Json(CalendarDataProvider.Provide(selectedMonth,offset));
        }

        [HttpPost]
        public IActionResult GetCalendarData(string selectedMonth)
        {
            return Json(CalendarDataProvider.Provide(selectedMonth));
        }
    }
}
