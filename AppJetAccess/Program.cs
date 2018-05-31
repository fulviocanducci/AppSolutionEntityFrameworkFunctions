using Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Jet;
using EntityFrameworkCore.Jet;

namespace AppJetAccess
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("* Access Jet Test                           *");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");

            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            DbContextOptionsBuilder<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>();
            var rootPath = Path
                .GetFullPath(".")
                .Replace("bin\\Debug", "");
            options.UseJet($"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={rootPath}database.accdb;");
            //options.UseJet($"Data Source={rootPath}database.mdb");
            options.UseLoggerFactory(loggerFactory);

            using (DatabaseContext db = new DatabaseContext(options.Options))
            {
                //db.Database.EnsureCreated();

                People p = new People();
                p.Name = "Name 3";
                p.IsActive = true;
                p.Salary = 30000;
                p.Created = DateTime.Parse("09/01/1999");
                db.People.Add(p);
                db.SaveChanges();

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
