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

        public IActionResult GetClick(string id)
        {
            return View();
        }

        [HttpGet]
        public IActionResult GetCalendarData(string selectedMonth)
        {
            Console.WriteLine($"*** {selectedMonth}");

            CalendarData calendarData = new();
            return Json(calendarData);
        }
    }
}
