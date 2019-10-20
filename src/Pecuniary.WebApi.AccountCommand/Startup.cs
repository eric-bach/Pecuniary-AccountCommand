﻿using System.Reflection;
using CQRS.Events.EventStore;
using CQRS.Events.Repository;
using Global.Common.Utilities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Pecuniary.WebApi.AccountCommand.Models;

namespace Pecuniary.WebApi.AccountCommand
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1)
                .AddJsonOptions(options => {
                    options.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;
                }); 
                
            RegisterServices(services);

            // Add MediatR
            services.AddMediatorHandlers(typeof(Startup).GetTypeInfo().Assembly);
        }
        
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }

        private static void RegisterServices(IServiceCollection services)
        {
            // ASP.NET HttpContext dependency
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Event Sourcing - toggle between InMemory and DynamoDB event stores
            //services.AddScoped<IEventStore, InMemoryEventStore>();
            services.AddScoped<IEventStore, DynamoDbEventStore>();

            // Command Service
            services.AddScoped<IEventRepository<Account>, EventRepository<Account>>();
            //services.AddScoped<AccountSnapshot, AccountSnapshot>();
        }
    }
}
