using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class MyRecord
    {
        public int Id { get; set; }
        public DateTime MyDateTime { get; set; }
        public string Text { get; set; }
        public User MyUser { get; set; }

        public RecordStatus Status { get; set; }

        public MyRecord(User myUser, DateTime myDateTime, string text, RecordStatus status)
        {
            MyUser = myUser;
            MyDateTime = myDateTime;
            Text = text;
            Status = status;
        }

        public MyRecord()
        {
        }
    }
    public enum RecordStatus 
    {
        ToStart, InProgress, Done
    }
}
