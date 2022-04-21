using Catalog.Infrastructure;
using Catalog.Application;
using Microsoft.OpenApi.Models;
using Shared.Api;

namespace Catalog.Api;

public class Startup
{
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
        services.AddApplication(Configuration);
        services.AddInfrastructure(Configuration);
        services.AddTransient<ExceptionMiddleware>();

        services.AddControllers();
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "CatalogApi", Version = "v1" });
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
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CatalogApi v1"));
        }

        app.ConfigExeptionMiddleware();

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