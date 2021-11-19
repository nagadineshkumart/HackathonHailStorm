using HailStormPredictionSystem.Extensions;
using HailStormPredictionSystem.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HailStormPredictionSystem
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
            // Bind database options. Invalid configuration will terminate the application startup.
            var connectionStringsOptions =
                Configuration.GetSection("ConnectionStrings").Get<ConnectionStringsOptions>();
            var cosmosDbOptions = Configuration.GetSection("CosmosDb").Get<CosmosDbOptions>();
            var (serviceEndpoint, authKey) = connectionStringsOptions.ActiveConnectionStringOptions;
            var (databaseName, collectionData) = cosmosDbOptions;
            var collectionNames = collectionData.Select(c => c.Name).ToList();
            // Add CosmosDb. This verifies database and collections existence.
            services.AddCosmosDb(serviceEndpoint, authKey, databaseName, collectionNames);
            ////services.AddCosmosDb(new Uri("https://vsg-iot-cosmosdb.documents.azure.com"), "Azure", databaseName, collectionNames);

            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "HailStormPredictionSystem", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "HailStormPredictionSystem v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
