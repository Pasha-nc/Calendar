using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.DAL
{
    public class UserRepository : GenericRepository<User>
    {
        public UserRepository(DbContext dbContext)
            : base(dbContext) { }
    }
}
