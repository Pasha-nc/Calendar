using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;

namespace WebApplication1.Models
{
    public static class RecordsHandler
    {
        public static async Task<bool> TryAddRecord(User myUser, DateTime myDateTime, string text)
        {
            using MyDbContext my = new();

            MyRecord myRecord = new(myUser, myDateTime, text);            

            var x = await my.Records.AddAsync(myRecord);

            if (x.State == Microsoft.EntityFrameworkCore.EntityState.Unchanged)
            {
                return true;

            }
            else
            { 
                return false; 
            }
        }
    }
}
