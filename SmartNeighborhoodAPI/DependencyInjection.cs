using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartNeighborhoodAPI.Configuration;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Services.Auth;
using SmartNeighborhoodAPI.Services.Factories;
using SmartNeighborhoodAPI.Services.IssueStatusHandlers;
using SmartNeighborhoodAPI.Services.Notifications;
using SmartNeighborhoodAPI.Services.ReportGenerators;
using Swashbuckle.AspNetCore.SwaggerGen;
using System;
using System.Net;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

namespace SmartNeighborhoodAPI
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // ── Row-Level Security infrastructure ────────────────────────────────────
            // IHttpContextAccessor lets CurrentUserService reach the JWT claims on every
            // request. Registered as a singleton by ASP.NET Core convention; the scoped
            // CurrentUserService reads from the ambient HttpContext at query time.
            services.AddHttpContextAccessor();
            services.AddScoped<ICurrentUserService, CurrentUserService>();

            services.AddRateLimiting();
            services.AddIssueStatusHandlers();
            services.AddNotifications();
            services.AddReportGenerators();

            return services;
        }

        public static IServiceCollection AddIssueStatusHandlers(this IServiceCollection services)
        {
            services.AddTransient<OpenIssueHandler>();
            services.AddTransient<InProgressIssueHandler>();
            services.AddTransient<ResolvedIssueHandler>();
            services.AddTransient<ClosedIssueHandler>();
            services.AddScoped<IIssueStatusHandlerFactory, IssueStatusHandlerFactory>();
            return services;
        }

        public static IServiceCollection AddNotifications(this IServiceCollection services)
        {
            services.AddTransient<PushNotificationSender>();
            services.AddTransient<EmailNotificationSender>();
            services.AddTransient<InAppNotificationSender>();
            services.AddSingleton<INotificationFactory, NotificationFactory>();
            return services;
        }

        public static IServiceCollection AddReportGenerators(this IServiceCollection services)
        {
            services.AddTransient<PdfReportGenerator>();
            services.AddTransient<ExcelReportGenerator>();
            services.AddTransient<CsvReportGenerator>();
            services.AddScoped<IReportGeneratorFactory, ReportGeneratorFactory>();
            return services;
        }

        public static IServiceCollection AddRateLimiting(this IServiceCollection services)
        {
            services.AddRateLimiter(options =>
            {
                options.AddPolicy("fixed-window", context =>
                    RateLimitPartition.GetFixedWindowLimiter(
                        context.Connection.RemoteIpAddress?.ToString() ?? "unknown",
                        factory: _ => new FixedWindowRateLimiterOptions
                        {
                            PermitLimit = 1000,
                            Window = TimeSpan.FromSeconds(1000),
                            QueueLimit = 0,
                            QueueProcessingOrder = QueueProcessingOrder.OldestFirst
                        }
                    )
                );

                options.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

                options.OnRejected = async (context, token) =>
                {
                    context.HttpContext.Response.ContentType = "application/json";

                    var response = ApiResponse<object>.Error(
                        HttpStatusCode.TooManyRequests,
                        "You have exceeded the allowed number of requests. Try again later."
                    );

                    var json = JsonSerializer.Serialize(response);
                    context.HttpContext.Response.StatusCode = (int)HttpStatusCode.TooManyRequests;

                    await context.HttpContext.Response.WriteAsync(json, token);
                };
            });
            return services;
        }

        /// <summary>
        /// Registers Swashbuckle with an enterprise-grade configuration:
        /// <list type="bullet">
        ///   <item>Schema ID conflicts eliminated via <c>FullName</c> strategy.</item>
        ///   <item>JWT Bearer security definition visible on every endpoint.</item>
        ///   <item>XML doc comments loaded automatically from the build output.</item>
        ///   <item>Annotation support enabled (SwaggerTag, SwaggerOperation, etc.).</item>
        /// </list>
        /// </summary>
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            // IConfigureOptions<SwaggerGenOptions> implementation that creates one
            // SwaggerDoc per API version (populated by AddVersionedApiExplorer).
            services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

            services.AddSwaggerGen(c =>
            {
                // ── TASK 1: SchemaId fix ─────────────────────────────────────────────────
                // Replacing "+" (nested types) with "." makes the id both unique and
                // human-readable in the Swagger UI model section.
                c.CustomSchemaIds(type =>
                    type.FullName!.Replace("+", "."));

                // ── TASK 2a: JWT Bearer security definition ──────────────────────────────
                var jwtScheme = new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,     // Http is cleaner than ApiKey
                    Scheme = "bearer",                    // must be lowercase for Http type
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description =
                        "Paste your JWT token below.\n\n" +
                        "The **Bearer** prefix is added automatically — do not include it.",
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                };

                c.AddSecurityDefinition("Bearer", jwtScheme);

                // Apply the requirement globally so every endpoint shows the lock icon.
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    { jwtScheme, Array.Empty<string>() }
                });

                // ── TASK 2b: XML comments ────────────────────────────────────────────────
                // GenerateDocumentationFile=true in the .csproj places the XML file next
                // to the assembly; we resolve the path at runtime to stay portable.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                if (File.Exists(xmlPath))
                    c.IncludeXmlComments(xmlPath, includeControllerXmlComments: true);

                // Enable [SwaggerTag], [SwaggerOperation], [SwaggerParameter] attributes.
                c.EnableAnnotations();
            });

            return services;
        }
    }
}
