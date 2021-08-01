using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PokedexApi.Core.Interfaces;
using PokedexApi.Core.Settings;
using PokedexApi.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PokedexApi.Extensions
{
    /// <summary>
    ///     Extension to handle Http client configuration
    /// </summary>
    public static class HttpClientConfigurationExtension
    {
        /// <summary>
        ///     Loads the required http clients for the Api
        /// </summary>
        /// <param name="services">The API service collection</param>
        public static void AddHttpClientConfiguration(this IServiceCollection services)
        {
            services.AddHttpClient<IPokedexService, PokedexService>((serviceProvider, client) =>
            {
                var settings = serviceProvider.GetRequiredService<IOptions<PokedexSettings>>().Value;
                if (string.IsNullOrEmpty(settings.BaseUrl))
                    throw new ArgumentNullException($"Base Url cannot be null or empty, please validate your {nameof(PokedexSettings)}");

                client.BaseAddress = new Uri(settings.BaseUrl);
                client.Timeout = TimeSpan.FromSeconds(settings.TimeoutSeconds);
            });
        }
    }
}
