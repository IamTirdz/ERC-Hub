using ERC.Hub.Business;
using ERC.Hub.Data;
using ERC.Hub.Function.Middlewares;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Abstractions;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Configurations;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Enums;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(app =>
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    })
    .ConfigureAppConfiguration((context, config) =>
    {
        config.AddEnvironmentVariables();

        if (context.HostingEnvironment.IsDevelopment())
        {
            config.AddJsonFile("local.settings.json", optional: true, reloadOnChange: true);
        }
    })
    .ConfigureServices((context, services) =>
    {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();

        services.AddDataService(context.Configuration);
        services.AddBusinessService();

        services.AddSingleton(provider => new JsonSerializerSettings
        {
            ContractResolver = new CamelCasePropertyNamesContractResolver()
        });

        services.AddHttpClient();
        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
            options.InvalidModelStateResponseFactory = context =>
            {
                return new ObjectResult(new { error = "Invalid request media type." })
                {
                    StatusCode = StatusCodes.Status415UnsupportedMediaType
                };
            };
        });

        services.AddSingleton<IOpenApiConfigurationOptions>(_ =>
        {
            var options = new OpenApiConfigurationOptions
            {
                Info = new OpenApiInfo
                {
                    Version = "1.0.0",
                    Title = "Employee Resource Center API",
                    Description = "A centralized hub providing employees with easy access to essential tools, information, and support for their professional growth and well-being."
                },
                Servers = DefaultOpenApiConfigurationOptions.GetHostNames(),
                OpenApiVersion = OpenApiVersionType.V2,
                IncludeRequestingHostName = true,
                ForceHttps = false,
                ForceHttp = false
            };

            return options;
        });
    })
    .ConfigureOpenApi()
    .Build();

host.Run();
