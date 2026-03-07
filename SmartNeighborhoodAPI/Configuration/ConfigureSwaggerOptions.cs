using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace SmartNeighborhoodAPI.Configuration
{
    /// <summary>
    /// Dynamically generates one SwaggerDoc per API version detected by
    /// <see cref="IApiVersionDescriptionProvider"/> and wires up JWT Bearer security
    /// so every versioned document supports "Authorize" in the Swagger UI.
    /// </summary>
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        private readonly IApiVersionDescriptionProvider _provider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider)
        {
            _provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            // --- One SwaggerDoc per API version ---
            foreach (var description in _provider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateVersionInfo(description));
            }
        }

        // ---------------------------------------------------------------
        // Helpers
        // ---------------------------------------------------------------

        private static OpenApiInfo CreateVersionInfo(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Title = "Smart Neighborhood API",
                Version = description.ApiVersion.ToString(),
                Description = description.IsDeprecated
                    ? "⚠️ This API version has been deprecated. Please migrate to the latest version."
                    : "A comprehensive API for managing smart neighborhood operations including families, blocks, conflicts, projects, and government institutions.",
                Contact = new OpenApiContact
                {
                    Name = "Smart Neighborhood Team",
                    Email = "support@smartneighborhood.com"
                },
                License = new OpenApiLicense
                {
                    Name = "MIT License",
                    Url = new Uri("https://opensource.org/licenses/MIT")
                }
            };

            return info;
        }
    }
}
