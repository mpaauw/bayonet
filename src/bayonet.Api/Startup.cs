using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector;

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
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc();
            app.UseCors(x => x.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());
        }
    }
}