using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using EmployeeCrud_Service.Data;
using EmployeeCrud_Service.Models.Domain;
using System.ComponentModel.DataAnnotations;

namespace EmployeeCrud_Service
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    builder.Configuration.
                    GetConnectionString("Default")));

            var app = builder.Build();

            //Apply migration at runtime
            //when docker sql server container is running so it receives data
            IApplicationBuilder applicationBuilder = app;
            using (IServiceScope scope = applicationBuilder.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var applicationDbContext = scope.ServiceProvider.GetService<ApplicationDbContext>();
                applicationDbContext.Database.EnsureCreated();
                Seeder seeder = new Seeder();
                seeder.Seed_Employees(applicationDbContext);
            }

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}