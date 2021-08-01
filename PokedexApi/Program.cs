using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace PokedexApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>()
                    .UseSerilog((ctx, cfg) =>
                    {
                        cfg.MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                            .Enrich.FromLogContext()
                            .Enrich.WithProperty("Application", ctx.HostingEnvironment.ApplicationName)
                            .Enrich.WithProperty("Environment", ctx.HostingEnvironment.EnvironmentName)
                            .WriteTo.Console(new RenderedCompactJsonFormatter());
                    });
                });
    }
}
