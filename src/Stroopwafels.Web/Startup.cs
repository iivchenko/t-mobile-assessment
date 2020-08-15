using System;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Stroopwafels.Application.Queries;
using Stroopwafels.Ordering;
using Stroopwafels.Ordering.Services;

namespace Stroopwafels.Web
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

            // TODO: hack. looks like mediator can't see handlers if rsponse types are in diffreent library
            services.AddMediatR(typeof(QuotesQueryHandler).Assembly);

            services.AddScoped<IHttpClientWrapper, HttpClientWrapper>();

            services.AddScoped<IStroopwafelSupplierService, StroopwafelSupplierAService>();
            services.AddScoped<IStroopwafelSupplierService, StroopwafelSupplierBService>();
            services.AddScoped<IStroopwafelSupplierService, StroopwafelSupplierCService>();
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
