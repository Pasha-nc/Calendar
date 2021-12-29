using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.DAL
{    
    public class RecordsRepository : GenericRepository<MyRecord>
    {
        public RecordsRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
