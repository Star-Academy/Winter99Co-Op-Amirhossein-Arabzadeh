using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nest;
using Phase10Library;
using SearchController = Phase10Library.SearchController;

namespace WebApplication
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
            var searchController = GetSearchController();

            services.Add(new ServiceDescriptor(typeof(SearchController), searchController)); 
            
            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "WebApplication", Version = "v1"});
            });
        }

        private SearchController GetSearchController()
        {
            var configuration = GetConfigurationRoot();

            var settings = configuration.Get<Settings>();

            var myElasticClient = GetMyElasticClient(settings);

            var elasticResponseValidator = new ElasticResponseValidator();

            var searchController = new Phase10Library.SearchController(myElasticClient, elasticResponseValidator, settings);
            return searchController;
        }

        private IElasticClient GetMyElasticClient(Settings settings)
        {
            IElasticClientFactory elasticClientFactory = new ElasticClientFactory();
            var myElasticClient = elasticClientFactory.CreateElasticClient(settings.Addresses.Host);
            return myElasticClient;
        }

        private IConfigurationRoot GetConfigurationRoot()
        {
            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", false, true)
                .Build();
            return configuration;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApplication v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}