using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeopleAPIServer;
using Scalar.AspNetCore;
using SMS.Domain.Enums;
using SMS.Application;
using SMS.Infrastructure;
using SMS.WebAPI.Mappings;
using System.Diagnostics;
using System.Text.Json.Serialization;
using SMS.Domain.Util;

namespace SMS.WebAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllers();

            builder.Services.AddOpenApi();

            builder.Services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            });

            builder.Services.AddControllers().ConfigureApiBehaviorOptions(options =>
            {
             options.InvalidModelStateResponseFactory = context =>
            {
               var errors = context.ModelState
                   .Where(e => e.Value?.Errors.Any() == true)
                   .SelectMany(e => e.Value!.Errors
                       .Select(err => new Error(
                           ErrorType.Validation,
                           "VALIDATION_ERROR",
                           err.ErrorMessage,
                           e.Key)))
                   .ToList();

               var traceId = Activity.Current?.TraceId.ToString()
                             ?? context.HttpContext.TraceIdentifier;

               return new BadRequestObjectResult(
                   ApiResponse.Build(400, traceId, errors));
           };
           });

            builder.Services.AddMiniProfiler(options =>
            {
                options.RouteBasePath = "/profiler"; //https://localhost:7185/profiler/results-index
            }).AddEntityFramework();

            Dependencies(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();

                app.MapScalarApiReference();

                app.UseMiniProfiler();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }

        public static void Dependencies(WebApplicationBuilder builder)
        {
            string connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException();

            builder.Services.AddApplication()
                .AddInfrastructure(connectionString);

            builder.Services.AddAutoMapper(cfg =>
            {
                cfg.AddProfile<PersonMappings>();
            });
        }
    }
}
