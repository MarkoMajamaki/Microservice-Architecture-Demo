using System.Reflection;
using Identity.Application;
using Identity.Infrastructure;
using MediatR;
using Microsoft.OpenApi.Models;

namespace Identity.Api
{
    public class Startup
    {
        private const string _corsPolicyName = "DevelopmentPolicy";

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options => 
            {
                options.AddPolicy(_corsPolicyName, builder => 
                {
                    builder
                    .AllowAnyOrigin()
                    .AllowAnyHeader();
                });
            });
            services.AddHttpClient();
            services.AddInfrastructure(Configuration);
            services.AddApplication(Configuration);
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo 
                { 
                    Title = "IdentityApi", Version = "v1" 
                });
            });
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "IdentityApi v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();

            if (env.IsDevelopment())
            {
                app.UseCors(_corsPolicyName);
            }

            app.UseAuthentication();  
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        } 
    }
}
