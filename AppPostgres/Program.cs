using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Linq;
using Log;
using Microsoft.Extensions.Logging;

namespace AppPostgres
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            DbContextOptionsBuilder<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>();
            options.UseNpgsql("Server=127.0.0.1;Port=5432;Database=postgres;User Id=postgres;Password=senha;");
            options.UseLoggerFactory(loggerFactory);
            using (DatabaseContext db = new DatabaseContext(options.Options))
            {
                //People p = new People();
                //p.Name = "Name 6";
                //p.IsActive = true;
                //p.Salary = 10000;
                //p.Created = DateTime.Parse("06/01/1999");
                //db.People.Add(p);
                //db.SaveChanges();

                db.People
                    .Where(x => x.Created.Day == 1)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name, 
                        x.Created
                    })
                    .ToList()
                    .ForEach(x =>
                    {
                        Console.WriteLine("{0:000} {1} {2}", x.Id, x.Name, x.Created);
                    });
            }

            Console.ReadKey();
        }
    }
}
