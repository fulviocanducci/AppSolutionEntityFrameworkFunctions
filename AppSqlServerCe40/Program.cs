using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using Log;
using Models;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AppSqlServerCe40
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("* SQLServer Compact40 Test                  *");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");

            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            DbContextOptionsBuilder<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>();
            var rootPath = Path
                .GetFullPath(".")
                .Replace("bin\\Debug", "");
            options.UseSqlCe($"Data Source={rootPath}database.sdf");
            options.UseLoggerFactory(loggerFactory);

            using (DatabaseContext db = new DatabaseContext(options.Options))
            {
                //db.Database.EnsureCreated();

                //People p = new People();
                //p.Name = "Name 3";
                //p.IsActive = true;
                //p.Salary = 10000;
                //p.Created = DateTime.Parse("07/01/1999");
                //db.People.Add(p);
                //db.SaveChanges();

                db.People
                    .Where(x => x.Created.Day >= 1)
                    .Select(x => new
                    {
                        x.Id,
                        x.Name,
                        x.Created,
                        Days = x.Created.AddDays(15)
                    })
                    .ToList()
                    .ForEach(x =>
                    {
                        Console.WriteLine("{0:000} {1} {2} {3}", x.Id, x.Name, x.Created, x.Days);
                    });
            }
        }
    }
}
