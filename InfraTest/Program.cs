using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SMS.Domain.Entities;
using SMS.Infrastructure;
using SMS.Infrastructure.Repositories;

namespace InfraTest;

class Program
{
    static async Task Main(string[] args)
    {
        var options = new DbContextOptionsBuilder<AppDbContext>()
         .UseSqlServer("Server=.;DataBase=School Management System;Integrated Security=True;TrustServerCertificate=True;")
         .LogTo(Console.WriteLine, LogLevel.Information)
         .EnableSensitiveDataLogging().Options;
          var context = new AppDbContext(options);

        var repo = new PersonRepository(context);

        var p = await repo.GetAllPeople(new Person(),1,1);
        
    }
}
