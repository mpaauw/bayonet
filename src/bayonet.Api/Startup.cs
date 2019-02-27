using bayonet.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using SimpleInjector;
using Swashbuckle.AspNetCore.Swagger;
using System.IO;

namespace bayonet.Api
{
    public class Startup
    {
        private readonly Container container;
        private readonly IConfiguration configuration;

        public Startup(IConfiguration configuration)
        {
            this.container = new Container();
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(Microsoft.AspNetCore.Mvc.CompatibilityVersion.Version_2_2);
            services.AddCors();

            services.AddSingleton<IWebService, WebService>();
            services.AddSwaggerGen(x =>
            {
                x.SwaggerDoc("v1", new Info { Title = "bayonet", Version = "v1" });

                //var path = Path.Combine(System.AppContext.BaseDirectory, "*.xml");
                string appPath = PlatformServices.Default.Application.ApplicationBasePath;
                foreach(string item in Directory.EnumerateFiles(appPath, "*.xml"))
                {
                    x.IncludeXmlComments(item);
                }
            });
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

            app.UseSwagger();
            app.UseSwaggerUI(x =>
            {
                x.SwaggerEndpoint("/swagger/v1/swagger.json", "bayonet");
            });
        }
    }
}