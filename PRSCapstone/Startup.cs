using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PRSCapstone.Models;

namespace PRSCapstone {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public readonly string DefaultCorsPolicy = "_defaultCorsPolicy";
        public string[] AllowOrigins = { "http://localhost:4200" };
        public string[] AllowMethods = { "GET", "PUT", "POST", "DELETE" };

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            services.AddControllers();
            services.AddDbContext<AppDbContext>(opt => {
                opt.UseLazyLoadingProxies();
                opt.UseSqlServer(Configuration.GetConnectionString("PRSDbContext"));
            });
            services.AddCors(opt =>
                opt.AddPolicy(DefaultCorsPolicy, x => x.WithOrigins(AllowOrigins).WithMethods(AllowMethods).AllowAnyHeader()
                )
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }
            app.UseCors(DefaultCorsPolicy);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
