using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleAPIServer;
using Scalar.AspNetCore;
using SMS.Business.PersonServices;
using SMS.Business.PersonServices.Mappings;
using SMS.Business.Util.ErrorCodes;
using SMS.DataAccess;
using SMS.WebAPI.Mappings;
using System.Diagnostics;
using static SMS.Business.Util.Result;

namespace SMS.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<MappingProfile>();
                cfg.AddProfile<PersonMappings>();
            });

            Dependencies(builder);

            builder.Services.AddControllers()
       .ConfigureApiBehaviorOptions(options =>
       {
           options.InvalidModelStateResponseFactory = context =>
           {
               var errors = context.ModelState
                   .Where(e => e.Value?.Errors.Any() == true)
                   .SelectMany(e => e.Value!.Errors
                       .Select(err => new ErrorDetail(
                           err.ErrorMessage,
                           ErrorCode.ValidationError,
                           e.Key)))
                   .ToList();

               var traceId = Activity.Current?.TraceId.ToString()
                             ?? context.HttpContext.TraceIdentifier;

               return new BadRequestObjectResult(
                   ApiResponse.Build(400, traceId, errors));
           };
       });

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static void Dependencies(WebApplicationBuilder builder)
        {
            string ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection")
           ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(ConnectionString));

            builder.Services.AddScoped<PersonRepository>();
            builder.Services.AddScoped<PersonService>();
            builder.Services.AddScoped<PersonValidator>();
        }
    }
}
