using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Xero.NetStandard.OAuth2.Config;
using XeroDemoApp.Domain.CommandHandlers;
using XeroDemoApp.Domain.Commands;
using XeroDemoApp.Domain.Commands.Results;
using XeroDemoApp.Domain.Entities;
using XeroDemoApp.Domain.Queries;
using XeroDemoApp.Domain.Queries.Results;
using XeroDemoApp.Domain.QueryHandlers;
using XeroDemoApp.Infrastructure.Data;
using XeroDemoApp.Infrastructure.Data.Abstractions;

namespace XeroDemoApp
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
            services.AddControllersWithViews();
            services.Configure<XeroConfiguration>(Configuration.GetSection("XeroConfiguration"));
            services.AddHttpClient();
            services.AddDistributedMemoryCache();
            services.AddSpaStaticFiles(configuration =>
            {
                configuration.RootPath = "ClientApp/dist";
            });
            services.AddMediatR(typeof(Startup));

            services.AddScoped<ITokenRepository, TokenRepository>();

            services.AddScoped<IRequestHandler<CreateAccessTokenCommand, CommandResult>, CreateAccessTokenCommandHandler>();
            services.AddScoped<IRequestHandler<DisconnectCommand, CommandResult>, DisconnectCommandHandler>();
            services.AddScoped<IRequestHandler<GetConnectionQuery, GetConnectionQueryResult>, GetConnectionQueryHandler>();
            services.AddScoped<IRequestHandler<GetLoginUriQuery, GetLoginUriQueryResult>, GetLoginUriQueryHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            if (!env.IsDevelopment())
            {
                app.UseSpaStaticFiles();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller}/{action=Index}/{id?}");
            });

            app.UseSpa(spa =>
            {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
