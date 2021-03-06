﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using WebProxyNETCore.Models;
using WebProxyNETCore.Service;

namespace WebProxyNETCore
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
            services.AddMvc();

            services.AddSingleton<IMongoDBService>(x =>
               new MongoDBService(
                   Configuration.GetValue<string>("MongoConStrDB"),
                   Configuration.GetValue<string>("MongoDefaultDB"),
                   Configuration.GetValue<string>("CollectionUsuarios"),
                   Configuration.GetValue<string>("CollectionConfigGrales"),
                   Configuration.GetValue<string>("CollectionConfigProxy"),
                   Configuration.GetValue<string>("CollectionInfoReqRes")
               ));

            services.AddSingleton<IRedisService>(x =>
            new RedisService(Configuration.GetConnectionString("Redis")));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
