using System;
using System.IO;
using System.Text.Json.Serialization;
using Api.Hubs;
using Api.Services.Implementations;
using Api.Services.Interfaces;
using Api.Util;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    public void ConfigureServices(IServiceCollection services)
    {
        services
            .AddControllers()
            .AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "Competition Status API",
                Description = @"API which provides status information for sports competition.
                        See https://github.com/Hekku2/competition-status-server/ for more information.",
                Version = "v1"
            });

            foreach (var name in Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.AllDirectories))
            {
                c.IncludeXmlComments(filePath: name);
            }

            c.CustomOperationIds(e =>
            {
                e.TryGetMethodInfo(out var methodInfo);
                return $"{e.ActionDescriptor.RouteValues["controller"]}{methodInfo.Name}";
            });
        });

        services.AddSignalR();
        services.AddSingleton<CompetitionService>();
        services.AddTransient<ICompetitionService>(serviceProvider => serviceProvider.GetRequiredService<CompetitionService>());
        services.AddTransient<ICompetitionStatusService>(serviceProvider => serviceProvider.GetRequiredService<CompetitionService>());
        services.AddTransient<IScoreboardService>(serviceProvider => serviceProvider.GetRequiredService<CompetitionService>());
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api v1"));

        }
        app.UseHttpLogging();

        //app.UseHttpsRedirection();

        app.UseRouting();

        app.UseCors(builder =>
        {
            var cors = Configuration.GetSection("Cors").Get<CorsSettings>();
            builder.WithOrigins(cors.AllowedOrigins.ToArray())
                .AllowAnyHeader()
                .WithMethods("GET", "POST", "PUT", "DELETE", "OPTIONS")
                .AllowCredentials();
        });

        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapHub<CompetitionStatusHub>("/competition-hub");
            endpoints.MapHub<ScoreboardHub>("/scoreboard-hub");
            endpoints.MapControllers();
        });
    }
}
