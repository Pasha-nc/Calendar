using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class CalendarData
    {
        public int SelectedDay { get; set; }
        public int SelectedMonth { get; set; }
        public int SelectedYear { get; set; }
        public int SelectedMonthStartingDay { get; set; }
        public int DaysInMonth { get; set; }
        public CalendarData()
        {

            DateTime currentDateTime = DateTime.Now;

            DateTime firstDayOfCurrentMonth = new(currentDateTime.Year, currentDateTime.Month, 1);

            int dayOfWeekOfFirstDayOfCurrentMonth = ((int)firstDayOfCurrentMonth.DayOfWeek);

            SelectedDay = currentDateTime.Day;
            SelectedMonth = currentDateTime.Month;
            SelectedYear = currentDateTime.Year;

            SelectedMonthStartingDay = dayOfWeekOfFirstDayOfCurrentMonth == 0 ? 7 : dayOfWeekOfFirstDayOfCurrentMonth - 1;

            DaysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
        }
    }
}
