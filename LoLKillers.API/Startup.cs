using LoLKillers.API.Configuration;
using LoLKillers.API.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoLKillers.API
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
            // Options pattern
            var config = Configuration.GetSection("AppConfig");
            services.AddOptions<AppConfig>().Bind(config);

            // dbContext
            services.AddDbContext<LoLKillersDbContext>(dbContextOptions =>
                dbContextOptions.UseSqlServer(config.GetConnectionString("PCConnection")));

            // Identity
            services.AddIdentityCore<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<LoLKillersDbContext>();

            // dependency injection stuff
            //todo: remove extraneous repos
            services.AddScoped<LoLKillersDbContext>();
            services.AddScoped<Interfaces.IConfigRepository, ConfigRepository>();
            services.AddScoped<Interfaces.IRiotApiRepository, RiotApiRepository>();
            services.AddScoped<Interfaces.IDatabaseRepository, DatabaseRepository>();

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
