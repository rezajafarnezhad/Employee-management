using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeMG.Core.Application.AmountOfSalary;
using EmployeeMG.Core.Application.BankAccount;
using EmployeeMG.Core.Application.Company;
using EmployeeMG.Core.Application.Cost;
using EmployeeMG.Core.Application.DepositSalary;
using EmployeeMG.Core.Application.Food;
using EmployeeMG.Core.Application.Personnel;
using EmployeeMG.Core.Application.Unit;
using EmployeeMG.Core.Application.Vacation;
using EmployeeMG.Core.ApplicationContract.AmountOfSalary;
using EmployeeMG.Core.ApplicationContract.BankAccount;
using EmployeeMG.Core.ApplicationContract.Company;
using EmployeeMG.Core.ApplicationContract.Cost;
using EmployeeMG.Core.ApplicationContract.DepositSalary;
using EmployeeMG.Core.ApplicationContract.Employee;
using EmployeeMG.Core.ApplicationContract.Food;
using EmployeeMG.Core.ApplicationContract.Unit;
using EmployeeMG.Core.ApplicationContract.Vacation;
using EmployeeMG.Core.Core;
using EmployeeMG.Data.Context;
using EmployeeMG.WebApp.Common.MessageBox;
using Microsoft.EntityFrameworkCore;

namespace EmployeeMG.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();

            services.AddMvc(options => options.ModelMetadataDetailsProviders.Add(new PersianDataAnnotationsCore.PersianValidationMetadataProvider()));
            services.AddDbContext<ApplicationContext>(c =>
            {
                c.UseSqlServer(Configuration.GetConnectionString("Connection"));
            });

            services.AddScoped<ICompanyApplication, CompanyApplication>();
            services.AddScoped<IAmountOfSalaryApplication,AmountOfSalaryApplication>();
            services.AddScoped<IUnitApplication,UnitApplication>();
            services.AddScoped<IEmployeeApplication,EmployeeApplication>();
            services.AddScoped<IVacationApplication,VacationApplication>();
            services.AddScoped<IBankAccountApplication, BankAccountApplication>();
            services.AddScoped<IDepositSalaryApplication,DepositSalaryApplication>();
            services.AddScoped<ICostApplication, CostApplication>();
            services.AddScoped<IFoodApplication,FoodApplication>();
            services.AddScoped<IUploadFile,FileUpload>();
            services.AddScoped<ImsgBox,msgBox>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
