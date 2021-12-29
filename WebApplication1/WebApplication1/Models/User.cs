using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class User
    {        
        public Guid Id { get; set; }
        public string Login { get; set; }
        public string PassHash { get; set; }
        public List<MyRecord> Records { get; set; }       
    }
}
