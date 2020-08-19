using System;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Refit;
using Stroopwafels.Application.Services;
using Stroopwafels.Infrastructure.Services.SupplierA;
using Stroopwafels.Infrastructure.Services.SupplierB;
using Stroopwafels.Infrastructure.Services.SupplierC;

namespace Stroopwafels.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());

            services
                .AddRefitClient<ISupplierAClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetValue<string>("Suppliers:SupplierAUrl")));

            services
                .AddRefitClient<ISupplierBClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetValue<string>("Suppliers:SupplierBUrl")));

            services
                .AddRefitClient<ISupplierCClient>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(Configuration.GetValue<string>("Suppliers:SupplierCUrl")));

            services.AddScoped<IStroopwafelSupplierService, StroopwafelSupplierAService>();
            services.AddScoped<IStroopwafelSupplierService, StroopwafelSupplierBService>();
            services.AddScoped<IStroopwafelSupplierService, StroopwafelSupplierCService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
