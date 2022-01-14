using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplication1.DAL;
using WebApplication1.Models;

namespace WebApplication1.Services
{
    public interface INotifier
    {
        void UseNotifier();
    }

    public abstract class Notifier : INotifier
    {
        IEnumerable<MyRecord> GetRecordsToNotify()
        {
            UnitOfWork unitOfWork = new();

            return unitOfWork.RecordRepo.Get().Where(r => (r.MyDateTime - DateTime.Now).TotalSeconds < 60
                                                       && (r.MyDateTime - DateTime.Now).TotalSeconds > 0);
        }

        void Notify()
        {
            while (true)
            {
                foreach (var item in GetRecordsToNotify())
                {
                    SendNotify(item);
                }

                Thread.Sleep(60000);
            }
        }

        public abstract void SendNotify(MyRecord my);

        public void UseNotifier()
        {
            ThreadStart notifyDelegate = new(Notify);
            Thread myThread = new(notifyDelegate);

            myThread.IsBackground = false;

            myThread.Start();
        }
    }

    public class ConsoleNotifier : Notifier
    {
        public override void SendNotify(MyRecord my)
        {
            Console.WriteLine($"***Alert*** {my.MyDateTime} {my.Title}");
        }
    }
}
