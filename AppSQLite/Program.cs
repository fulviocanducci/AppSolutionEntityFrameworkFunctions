using System;
using Models;
using System.Linq;
using Log;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using System.IO;

namespace AppSQLite
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("* SQLite Test                               *");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");

            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            DbContextOptionsBuilder<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>();
            var rootPath = Path
                .GetFullPath(".")
                .Replace("bin\\Debug\\netcoreapp2.0","");
            options.UseSqlite($"Data Source={rootPath}database.db");
            options.UseLoggerFactory(loggerFactory);

            using (DatabaseContext db = new DatabaseContext(options.Options))
            {
                db.Database.EnsureCreated();
                db.People
                    .Where(x => x.Created.Day == 1)
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

            Console.ReadKey();
        }
    }
}
