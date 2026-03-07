using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MramProject.Interface;
using MramProject.Services;
using OurProjectSmartNeiborhood.Services;
using Serilog;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Middlewares;
using SmartNeighborhoodAPI.Services;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RemoteConnection")));


//builder.Services.AddLocalization(options =>
//{
//    options.ResourcesPath = "Resources";
//});
//builder.Services.Configure<RequestLocalizationOptions>(options =>
//{
//    var supportedCultures = new[]
//    {
//        new CultureInfo("en-US"),
//        new CultureInfo("ar-SA")
//    };
//    options.DefaultRequestCulture = new RequestCulture("ar-SA");
//    options.SupportedCultures = supportedCultures;
//});

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();


builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<JWT>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<IAuthService, AuthService>();

builder.Services.AddScoped<IPersonService, PersonService>();
builder.Services.AddScoped<IFamilyCatgoryService, FamilyCatgoryService>();
builder.Services.AddScoped<IFamilyService, FamilyService>();
builder.Services.AddScoped<IMemberFamilyRoleService, MemberFamilyRoleService>();
builder.Services.AddScoped<IBlockServices, BlockServices>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<IConflictCaseService, ConflictCaseService>();
builder.Services.AddScoped<IConflictTypeService, ConflictTypeService>();
builder.Services.AddScoped<IProjectCatgoryService, ProjectCatgoryService>();
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IProjectFamilieservice, ProjectFamilieservice>();
builder.Services.AddScoped<ITeamsService, TeamsService>();
builder.Services.AddScoped<ITeamMemberService, TeamMemberService>();
builder.Services.AddScoped<IFamilyMemberService, FamilyMemberService>();
builder.Services.AddScoped<ITeamRoleService, TeamRoleService>();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<UserContextService>();
builder.Services.AddScoped<IGovernmentInstitutionsService, GovernmentInstitutionsService>();
builder.Services.AddScoped<IGovernmentInstitutionContactService, GovernmentInstitutionContactService>();
builder.Services.AddScoped<IResidentialNeighborhoodService, ResidentialNeighborhoodService>();
builder.Services.AddScoped<ResidentialUnitService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IIssueService, IssueService>();
builder.Services.AddApplicationServices(builder.Configuration);



builder.Host.UseSerilog((context, loggerConfig) =>
loggerConfig.ReadFrom.Configuration(context.Configuration));

builder.WebHost.UseSentry(options =>
{
    options.Dsn = "https://a8e654fb302d229b35e1ae60d8e9838d@o4509708628525056.ingest.us.sentry.io/4509708647923712";
    options.TracesSampleRate = 1.0; // Capture performance traces (100%)
    options.SendDefaultPii = true; // Send user info automatically (if available)
    options.Debug = true; // Turn off debug for production
});

// rate limiting is configured inside AddApplicationServices; no need to duplicate here


var jwt = builder.Configuration.GetSection("Jwt").Get<JWT>();

if (string.IsNullOrWhiteSpace(jwt?.SigningKey))
    throw new InvalidOperationException("JWT SigningKey is missing or empty in configuration.");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(e =>
{
    e.RequireHttpsMetadata = false;
    e.SaveToken = false;
    e.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwt.Issuer,
        ValidAudience = jwt.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.SigningKey))
    };
});
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);

builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
});

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.PropertyNamingPolicy = System.Text.Json.JsonNamingPolicy.CamelCase;
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

builder.Services.AddEndpointsApiExplorer();

// ── API Versioning ─────────────────────────────────────────────────────────
// AddVersionedApiExplorer registers IApiVersionDescriptionProvider, which is
// required by ConfigureSwaggerOptions to create one SwaggerDoc per version.
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true; // adds "api-supported-versions" header
})
.AddVersionedApiExplorer(options =>
{
    // Format: 'v' + major[.minor] — e.g. v1, v1.1
    options.GroupNameFormat           = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
});

// Registers Swagger with bulletproof SchemaIds, JWT security, and XML comments.
// See DependencyInjection.cs ➜ AddSwaggerDocumentation() for the full setup.
builder.Services.AddSwaggerDocumentation();
var app = builder.Build();
app.UseRequestLocalization();
//app.UseMiddleware<ExceptionHandlingMiddleware>();

// ── Swagger ────────────────────────────────────────────────────────────────
// Exposed in all environments so the hosted API can also be explored.
// Scope to IsDevelopment() only if you want to hide docs in production.
app.UseSwagger(c =>
{
    // Ensure the spec is served at the default path (/swagger/{documentName}/swagger.json)
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(options =>
{
    // Serve the UI at /swagger — matches launchSettings.json "launchUrl": "swagger"
    options.RoutePrefix = "swagger";

    // Dynamically register one dropdown entry per API version so adding a new
    // version in the future requires zero changes here.
    var provider = app.Services.GetRequiredService<Microsoft.AspNetCore.Mvc.ApiExplorer.IApiVersionDescriptionProvider>();
    foreach (var description in provider.ApiVersionDescriptions.OrderByDescending(v => v.ApiVersion))
    {
        var label = description.IsDeprecated
            ? $"Smart Neighborhood API {description.GroupName} (deprecated)"
            : $"Smart Neighborhood API {description.GroupName}";

        options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", label);
    }

    // ── Enterprise UI options ───────────────────────────────────────────────
    // Keep the JWT token alive across browser refreshes.
    options.EnablePersistAuthorization();

    // Collapse all operation groups on first load for a cleaner overview.
    options.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);

    // Surface request duration in the UI.
    options.DisplayRequestDuration();

    // Shareable deep links per operation.
    options.EnableDeepLinking();
});

app.UseRateLimiter();
app.UseMiddleware<RequestTimingMiddleware>();

app.UseStaticFiles();
app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();