using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PokedexApi.Core.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexApi.Extensions
{
    /// <summary>
    ///     Extension to handle Api configuration
    /// </summary>
    public static class ConfigurationExtension
    {
        /// <summary>
        ///     Loads all configuration values from the application settings
        /// </summary>
        /// <param name="services">The API service collection</param>
        /// <param name="configuration">The application configuration properties</param>
        public static void AddConfigurationOptions(this IServiceCollection services, IConfiguration configuration)
        {
            #region ConfigurationOptions

            services.Configure<PokedexServiceSettings>(configuration.GetSection(nameof(PokedexServiceSettings)));
            services.Configure<TranslationServiceSettings>(configuration.GetSection(nameof(TranslationServiceSettings)));

            #endregion ConfigurationOptions
        }
    }
}
