using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Hosting;

namespace aspnetapp
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
                    webBuilder.ConfigureKestrel(options =>
                    {
                        //Port 80 may not be bindable.
                        //For local testing purposes, please use port 4999 instead.
                        //options.ListenAnyIP(80, listenOptions =>
                        //{
                        //    listenOptions.Protocols = HttpProtocols.Http1;
                        //});

                        options.ListenAnyIP(4999, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1;
                        });

                        options.ListenAnyIP(5000, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http2;
                        });
                    })
                    .UseStartup<Startup>();
                });
    }
}
