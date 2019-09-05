using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetSecurity_NoSecurity.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AspNetSecurity_NoSecurity
{
    public class Startup
    {
        private readonly IHostingEnvironment environment;

        public Startup(IHostingEnvironment environment)
        {
            this.environment = environment;
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc();

            if (!environment.IsDevelopment())
            {
                services.Configure<MvcOptions>(o => o.Filters.Add(new RequireHttpsAttribute()));
            }

            services.AddSingleton<ConferenceRepo>();
            services.AddSingleton<ProposalRepo>();
            services.AddSingleton<AttendeeRepo>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, ILoggerFactory loggerFactory)
        {
            app.UseDeveloperExceptionPage();

            app.UseHsts(x => x.MaxAge(days: 365).Preload());
            app.UseStaticFiles();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Conference}/{action=Index}/{id?}");
            });
        }
    }
}
