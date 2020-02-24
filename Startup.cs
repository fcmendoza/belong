using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace belong
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
            // services.Add(new ServiceDescriptor(typeof(IHostRepository), new HostInMemoryRepository()));
            services.AddDbContext<BelongDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("BelongDb")));
            // services.AddScoped<IHostRepository, HostSqlRepository>();
            services.AddScoped<IHostRepository, HostDapperRepository>();
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            _logger = logger;

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                _logger.LogInformation("This is dev.");
            }

            app.Use(async (context, next) => {
                var baseUrl = $"{context.Request.Scheme}://{context.Request.Host}{context.Request.PathBase}";
                _logger.LogInformation("Base URL: {baseUrl}", baseUrl);
                UrlBase.Set(baseUrl);
                await next();
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private ILogger<Startup> _logger;
    }

    public static class UrlBase {
        static string _baseUrl;

        public static void Set(string baseUrl) {
            _baseUrl = baseUrl;
        }
        public static string Get() {
            return _baseUrl;
        }
    }
}
