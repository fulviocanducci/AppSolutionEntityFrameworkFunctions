﻿using System;
using System.Linq;
using Log;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Models;
namespace AppSqlServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");
            Console.WriteLine("* SQLServer Test                            *");
            Console.WriteLine("*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*-*");

            LoggerFactory loggerFactory = new LoggerFactory();
            loggerFactory.AddProvider(new TraceLoggerProvider());
            DbContextOptionsBuilder<DatabaseContext> options = new DbContextOptionsBuilder<DatabaseContext>();
            options.UseSqlServer("Server=.\\SqlExpress;Database=DataBase;User Id=sa;Password=senha;MultipleActiveResultSets=true;");
            options.UseLoggerFactory(loggerFactory);

            using (DatabaseContext db = new DatabaseContext(options.Options))
            {

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

            Console.ReadKey();
        }
    }
}
