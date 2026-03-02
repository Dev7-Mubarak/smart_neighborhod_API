using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Services.Factories;
using SmartNeighborhoodAPI.Services.IssueStatusHandlers;
using SmartNeighborhoodAPI.Services.Notifications;
using SmartNeighborhoodAPI.Services.ReportGenerators;
using System;
using System.Net;
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





    }
}
