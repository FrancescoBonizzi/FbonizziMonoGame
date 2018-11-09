using FlowsoftGamesLeaderboardApi.Data;
using FlowsoftGamesLeaderboardApi.Data.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.Extensions.DependencyInjection;

namespace FlowsoftGamesLeaderboardApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            const string sqlConnectionString = "ABC";

            services.AddMvc();
            services
                .AddSingleton<ILeaderboardRepository>(new SqlServerLeaderboardRepository(sqlConnectionString))
                .AddSingleton<ICache, InMemoryCache>();
            
#if DEBUG
            services.AddSingleton<ILogger, ConsoleLogger>();
#else
            services.AddSingleton<ILogger>(new SqlServerLogger(sqlConnectionString));
#endif

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app
                .UseDeveloperExceptionPage()
                .UseStaticFiles()
                .UseDefaultFiles()
                .UseForwardedHeaders(new ForwardedHeadersOptions
                {
                    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto
                })
                .UseMvc(routes =>
                {
                    routes.MapRoute(
                        name: "default",
                        template: "{controller=Pages}/{action=Index}/{id?}");
                });
        }
    }
}
