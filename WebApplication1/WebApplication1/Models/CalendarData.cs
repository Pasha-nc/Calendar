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
        public CalendarData(DateTime currentDateTime)
        {
            DateTime firstDayOfCurrentMonth = new(currentDateTime.Year, currentDateTime.Month, 1);

            int dayOfWeekOfFirstDayOfCurrentMonth = ((int)firstDayOfCurrentMonth.DayOfWeek);

            SelectedDay = currentDateTime.Day;
            SelectedMonth = currentDateTime.Month;
            SelectedYear = currentDateTime.Year;

            SelectedMonthStartingDay = dayOfWeekOfFirstDayOfCurrentMonth == 0 ? 6 : dayOfWeekOfFirstDayOfCurrentMonth - 1;

            DaysInMonth = DateTime.DaysInMonth(SelectedYear, SelectedMonth);
        }        
    }

    public static class CalendarDataProvider
    {
        public static CalendarData Provide(int year, int month, int offset)
        {
            if (offset == 1 || offset == -1)
            {
                if (month + offset < 1)
                {
                    year--;
                    month = 12;
                    offset = 0;
                }
                else if (month + offset > 12)
                {
                    year++;
                    month = 1;
                    offset = 0;
                }
                else
                {
                    ;
                }
            }

            return new(new DateTime(year, month + offset, 1));
        }
        public static CalendarData Provide(int year, int month)
        {
            return new((year == DateTime.Now.Year && month == DateTime.Now.Month) ? 
                                DateTime.Now : new DateTime(year, month, 1));
        }
    }

    public enum MyDaysOfWeek
    {
        Monday, Tuesday, Wednesday, Thursday, Friday, Saturday, Sunday
    }
}
