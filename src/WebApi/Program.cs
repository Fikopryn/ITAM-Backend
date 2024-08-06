using Core.Helpers;
using Core.Models;
using Domain;
using Infrastructure;
using Infrastructure.Filters.SwaggerUI;
using Microsoft.AspNetCore.Builder;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Sinks.Elasticsearch;
using System.Text.Json.Serialization;

namespace WebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Host.UseSerilog();
            var appConfigs = builder.Configuration.GetSection("AppSettingsConfig").Get<AppSettingsConfig>();

            Log.Logger = new LoggerConfiguration()
                        .Enrich.FromLogContext()
                        .Enrich.WithMachineName()
                        .Enrich.WithProperty("appname", appConfigs.AppName)
                        .WriteTo.Debug()
                        .WriteTo.Console()
                        .WriteTo.Elasticsearch(ConfigureElasticSink(builder.Configuration, appConfigs.AppEnv))
                        .ReadFrom.Configuration(builder.Configuration)
                        .CreateLogger();
            //Log.Information("{@LogData}", LogData.BuildErrorLog("hp", SystemHelper.GetActualAsyncMethodName(), "startup log test"));

            Log.Information("Application Started Up");

            var conString = builder.Configuration.GetConnectionString("DefaultConnection");
            var encryptPass = builder.Configuration.GetConnectionString("DefaultEncryptedPassword");

#pragma warning disable CS8602 // Dereference of a possibly null reference.
            builder.Services
                .AddAppDbContexts(conString.Replace("@password", EncryptionHelper.AesDecrypt(encryptPass)))
                .AddAppServices()
                .AddAppExternals()
                .AddAppSettings(builder.Configuration);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

            if (appConfigs != null)
            {
                if (!string.IsNullOrWhiteSpace(appConfigs.CorsUrl))
                {
                    builder.Services.AddCors(options =>
                    {
                        options.AddPolicy("R3CorsPolicy", builder =>
                        {
                            builder
                                .AllowAnyHeader()
                                .AllowCredentials()
                                .WithOrigins(appConfigs.CorsUrl.Split(';'))
                                .WithMethods("OPTIONS", "GET", "POST", "PUT", "PATCH", "DELETE");
                        });
                    });
                }
            }

            builder.Services
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                });

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AssetManagement API", Version = "v1" });
                c.EnableAnnotations();

                c.SchemaFilter<EnumSchemaFilter>();
                c.OperationFilter<CustomHeaderFilter>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "Bearer"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });
            });

            builder.Services.AddAutoMapper(typeof(AutoMapProfile));

            var app = builder.Build();

            app.UsePathBase(appConfigs.ApplicationUrl);

            //if (app.Environment.IsDevelopment())
            //{
                app.UseSwagger();
                app.UseSwaggerUI(options =>
                {
                    options.SwaggerEndpoint("/swagger/v1/swagger.json", "AssetManagement API V1");
                    options.DefaultModelsExpandDepth(-1);

                    options.RoutePrefix = "";
                });
            //}

            app.UseCors("R3CorsPolicy");
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.Run();
        }

        private static ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
        {
            return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
            {
                AutoRegisterTemplate = true,
                IndexFormat = $"br3wok-{environment?.ToLower()}-{DateTime.UtcNow:yyyy.MM.dd}"
            };
        }
    }
}