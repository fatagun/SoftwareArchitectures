using MediatorApi.Data;
using MediatorApi.Domain;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using AutoMapper;

namespace MediatorApi
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
            services.AddMediatR();
            services.AddAutoMapper();

            services.AddDbContext<ApplicationContext>(option => option.UseInMemoryDatabase("Customer"));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            var context = serviceProvider.GetService<ApplicationContext>();

            context.Customers.Add(new Customer { Id = 1, Age = 24, Company = "Yahoo!", FavoriteDrink = "tea", Name = "John", Lastname = "Doe" });
            context.Customers.Add(new Customer { Id = 2, Age = 25, Company = "IBM", FavoriteDrink = "latte", Name = "Jane", Lastname = "Doe" });
            context.SaveChanges();

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
