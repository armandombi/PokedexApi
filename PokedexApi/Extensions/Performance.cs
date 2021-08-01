using System.IO.Compression;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.Extensions.DependencyInjection;

namespace PokedexApi.Extensions
{
    /// <summary>
    ///     Extension to handle performance configuration for the API
    /// </summary>
    internal static class PerformanceExtension
    {
        /// <summary>
        ///     Adds configurations to help API performance
        /// </summary>
        /// <param name="services">The API service collection</param>
        internal static void AddPerformance(this IServiceCollection services)
        {
            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
                options.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(new[] { "image/svg+xml" });
            });


            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });
        }

        internal static void UsePerformance(this IApplicationBuilder app)
        {
            app.UseResponseCompression();
        }
    }
}
