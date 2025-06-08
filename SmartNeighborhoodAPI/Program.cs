using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using OurProjectSmartNeiborhood.Services;
using SmartNeighborhoodAPI.Entites;
using SmartNeighborhoodAPI.Interfaces;

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
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<AppUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<FamilyCatgoryService>();

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.Configure<JWT>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<PersonService>();
builder.Services.AddScoped<ChatService>();
builder.Services.AddScoped<FamilyCatgoryService>();
builder.Services.AddScoped<FamilyTypeService>();
builder.Services.AddScoped<FamilyService>();
builder.Services.AddScoped<MemberFamilyRoleService>();
builder.Services.AddScoped<BlockServices>();
builder.Services.AddScoped<AdsService>();
builder.Services.AddScoped<ImageService>();
builder.Services.AddScoped<GroupService>();
builder.Services.AddScoped<ComplainService>();
builder.Services.AddScoped<ComplainTypeService>();
builder.Services.AddScoped<ConfilctPartyService>();
builder.Services.AddScoped<ProjectCatgoryService>();
builder.Services.AddScoped<ProjectService>();
builder.Services.AddScoped<ProjectFamilieservice>();
builder.Services.AddScoped<TeamsService>();
builder.Services.AddScoped<TeamMemberService>();
builder.Services.AddScoped<FamilyMemberService>();



//var jwt = builder.Configuration.GetSection("Jwt").Get<JWT>();
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//    .AddJwtBearer(e =>
//    {
//        e.RequireHttpsMetadata = false;
//        e.SaveToken = false;
//        e.TokenValidationParameters = new TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = jwt?.Issuer,
//            ValidAudience = jwt?.Audience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt?.signingKey ?? ""))
//        };
//    });

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
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();
//app.UseMiddleware<ExceptionHandlingMiddleware>();


// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI(options =>
{
    options.RoutePrefix = "swagger";
});
app.UseStaticFiles(); 

app.UseCors("AllowAll");
app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
