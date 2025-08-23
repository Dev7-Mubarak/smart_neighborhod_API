using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using OurProjectSmartNeiborhood.Services;
using Serilog;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Interfaces;
using SmartNeighborhoodAPI.Middlewares;
using System.Globalization;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddApplicationServices(builder.Configuration);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = new UrlSegmentApiVersionReader();
});

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("RemoteConnection")));
builder.Services.AddLocalization(options =>
{
    options.ResourcesPath = "Resources";
});
builder.Services.Configure<JWT>(builder.Configuration.GetSection("Jwt"));
builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();
builder.Services.Configure<IdentityOptions>(options =>
{
    options.Password.RequireDigit = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequiredLength = 8;
});
var jwt = builder.Configuration.GetSection("Jwt").Get<JWT>();

if (string.IsNullOrWhiteSpace(jwt?.signingKey))
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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.signingKey))
    };
});
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    var supportedCultures = new[]
    {
        new CultureInfo("en-US"),
        new CultureInfo("ar-SA")
    };
    options.DefaultRequestCulture = new RequestCulture("ar-SA");
    options.SupportedCultures = supportedCultures;
});
builder.Host.UseSerilog((context, loggerConfig) =>
loggerConfig.ReadFrom.Configuration(context.Configuration));
// ✅ Configure Sentry for Production
builder.WebHost.UseSentry(options =>
{
    options.Dsn = "https://a8e654fb302d229b35e1ae60d8e9838d@o4509708628525056.ingest.us.sentry.io/4509708647923712";
    options.TracesSampleRate = 1.0; // Capture performance traces (100%)
    options.SendDefaultPii = true; // Send user info automatically (if available)
    options.Debug = true; // Turn off debug for production
});




builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));
builder.Services.AddTransient<IEmailSender, EmailSender>();

builder.Services.Configure<ApiBehaviorOptions>(options =>
    options.SuppressModelStateInvalidFilter = true);


builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // v1, v1.1, etc.
    options.SubstituteApiVersionInUrl = true;
});

builder.Services.AddSwaggerGen(c =>
{
    // Add JWT Authentication to Swagger
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token.\n\nExample: \"Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6...\"",
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
            new string[] {}
        }
    });
});

builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<FamilyCatgoryService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<JWT>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IPersonService,PersonService>();
builder.Services.AddScoped<IFamilyCatgoryService,FamilyCatgoryService>();
builder.Services.AddScoped<IFamilyTypeService,FamilyTypeService>();
builder.Services.AddScoped<IFamilyService,FamilyService>();
builder.Services.AddScoped<IMemberFamilyRoleService,MemberFamilyRoleService>();
builder.Services.AddScoped<IBlockServices,BlockServices>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<IConflictCaseService, ConflictCaseService>();
builder.Services.AddScoped<IConflictTypeService, ConflictTypeService>();
builder.Services.AddScoped<IProjectCatgoryService,ProjectCatgoryService>();
builder.Services.AddScoped<IProjectService,ProjectService>();
builder.Services.AddScoped<IProjectFamilieservice,ProjectFamilieservice>();
builder.Services.AddScoped<ITeamsService,TeamsService>();
builder.Services.AddScoped<ITeamMemberService,TeamMemberService>();
builder.Services.AddScoped<IFamilyMemberService,FamilyMemberService>();
builder.Services.AddScoped<ITeamRoleService,TeamRoleService>();
var app = builder.Build();
app.UseRequestLocalization();
//app.UseMiddleware<ExceptionHandlingMiddleware>();


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "swagger";
});

app.UseRateLimiter();
app.UseMiddleware<RequestTimingMiddleware>();

app.UseStaticFiles();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwaggerUI(options =>
    {
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });

};
app.UseHttpsRedirection();

app.UseCors("AllowAll");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
