using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NeuralNetworkAPI.Utils;

namespace NeuralNetworkAPI
{
    public class Startup
    {
        private readonly string CORS_POLICY = "customCorsPolicy";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Settings.Initialize(Configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => {
                options.AddPolicy(
                    name: CORS_POLICY,
                    builder => {
                        builder.AllowAnyHeader();
                        builder.AllowAnyOrigin();
                        builder.AllowAnyMethod();
                    }
                );
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseCors(CORS_POLICY);
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
