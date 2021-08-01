using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.PlatformAbstractions;
using PokedexApi.Core.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace PokedexApi.Extensions
{
    /// <summary>
    ///     Extension to handle swagger configuration
    /// </summary>
    public static class SwaggerExtension
    {
        private static string XmlCommentsFilePath {
            get {
                var basePath = PlatformServices.Default.Application.ApplicationBasePath;
                var fileName = typeof(Startup).GetTypeInfo().Assembly.GetName().Name + ".xml";
                return Path.Combine(basePath, fileName);
            }
        }

        /// <summary>
        ///     Adds swagger configuration to the API service configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerOptions(this IServiceCollection services)
        {
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
            services.AddSwaggerGen(
                options =>
                {
                    options.OperationFilter<SwaggerDefaultValues>();
                    options.IncludeXmlComments(XmlCommentsFilePath);
                });
        }

        /// <summary>
        ///     Adds swagger settings to the request pipeline
        /// </summary>
        /// <param name="app">The API application builder</param>
        /// <param name="provider">
        ///     The <see cref="IApiVersionDescriptionProvider">provider</see> used to generate Swagger
        ///     documents.
        /// </param>
        public static void UseSwaggerGen(this IApplicationBuilder app, IApiVersionDescriptionProvider provider)
        {
            app.UseSwagger();
            app.UseSwaggerUI(
                options =>
                {
                    for (var i = provider.ApiVersionDescriptions.Count - 1; i >= 0; i--)
                    {
                        var description = provider.ApiVersionDescriptions[i];
                        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                            $"Pokedex API {description.GroupName}");
                    }
                });
        }
    }
}
