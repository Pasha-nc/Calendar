using Microsoft.Extensions.Hosting;
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
        void Notify();
    }

    public abstract class Notifier : INotifier
    {
        IEnumerable<MyRecord> GetRecordsToNotify()
        {
            UnitOfWork unitOfWork = new();
            //TODO Optimize query
            return unitOfWork.RecordRepo.Get().Where(r => (r.MyDateTime - DateTime.Now).TotalSeconds < 60
                                                               && (r.MyDateTime - DateTime.Now).TotalSeconds > 0);
        }

        public void Notify()
        {
            //Console.WriteLine($"Notifier is called {DateTime.Now}");
            foreach (var item in GetRecordsToNotify())
            {
                SendNotify(item);
            }
        }

        public abstract void SendNotify(MyRecord my);
    }

    public class ConsoleNotifier : Notifier
    {
        public override void SendNotify(MyRecord my)
        {
            Console.WriteLine($"***Alert*** {my.MyDateTime} {my.Title}");
        }
    }

    public class NotifierHostedService : IHostedService, IDisposable
    {
        int executionCount = 0;
        Timer _timer = null!;
        INotifier myNotifier;

        public NotifierHostedService(INotifier notifier)
        {
            myNotifier = notifier;
        }

        public Task StartAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Notifier Hosted Service running.");

            _timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromSeconds(60));

            return Task.CompletedTask;
        }

        private void DoWork(object state)
        {
            var count = Interlocked.Increment(ref executionCount);

            myNotifier.Notify();

            //Console.WriteLine($"Timed Hosted Service is working. Count: {count} {DateTime.Now}");
        }

        public Task StopAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Notifier Hosted Service is stopping.");

            _timer?.Change(Timeout.Infinite, 0);

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}
