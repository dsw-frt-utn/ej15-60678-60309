using Dsw2026Ej15.Api.Middlewares;
using Dsw2026Ej15.Data.Sources;
using Dsw2026Ej15.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Dsw2026Ej15
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi

            //builder.Services.AddOpenApi();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            //builder.Services.AddSingleton<IPersistence, PersistenceInMemory>(); //Usabamos antes cuando era Singleton

            //Parte nueva para BD
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
            builder.Services.AddScoped<IPersistence, PersistenceEf>();


            var app = builder.Build();

            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                //app.MapOpenApi();
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
