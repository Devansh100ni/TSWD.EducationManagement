using EvolveDb;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Reflection;
using System.Text;
using TSWD.EducationManagement;
using TSWD.EducationManagement.EntityFrameworkCore;
using TSWD.EducationManagement.EntityFrameworkCore.Infrastructure;
using TSWD.EducationManagement.Permissions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();



if (OperatingSystem.IsWindows())
{
    builder.Logging.AddEventLog();
}

var environment = builder.Environment;
string connectionString = environment.IsDevelopment()
    ? builder.Configuration.GetConnectionString("LocalConnection")
    : builder.Configuration.GetConnectionString("ProductionConnection");

builder.Configuration["ConnectionStrings:DefaultConnection"] = connectionString;

// Register DbContext (example)
builder.Services.AddDbContext<EducationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddHttpContextAccessor();

var assemblies = new List<Assembly>
{
    Assembly.Load("TSWD.EducationManagement.Application"),
    Assembly.Load("TSWD.EducationManagement.Dapper"),
    Assembly.Load("TSWD.EducationManagement.EntityFrameworkCore"),
    // Add more projects as needed
};

foreach (var assembly in assemblies)
{
    builder.Services.AddAllServicesFromAssembly(assembly);
}

// Generic repositories
builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));


builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "My API",
        Version = "v1",
        Description = "Example .NET 9 Web API with Swagger",
        Contact = new OpenApiContact
        {
            Name = "Your Name",
            Email = "you@example.com"
        }
    });
    options.AddSecurityDefinition("oauth2", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Standard Authorization header using the Bearer Scheme(\"bearer {token}\"",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    options.OperationFilter<SecurityRequirementsOperationFilter>();
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        builder =>
        {
            builder.WithOrigins("http://educationmanagement-api.somee.com").WithOrigins("http://localhost:4200")
            .AllowAnyHeader()
            .AllowAnyMethod().AllowAnyOrigin();
        });
});

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(o =>
{
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    };
});

var app = builder.Build();

ConfigureEvolve(builder.Configuration, app);
SeedPermissions(app);

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "My API v1");
        options.RoutePrefix = string.Empty; // Swagger at root URL
    });
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseCors("AllowSpecificOrigin");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();


static void ConfigureEvolve(IConfiguration configuration, WebApplication app)
{
    try
    {
        string connectionString;

        if (app.Environment.IsDevelopment())
        {
            connectionString = configuration.GetConnectionString("LocalConnection");
        }
        else
        {
            connectionString = configuration.GetConnectionString("ProductionConnection");
        }

        using var cnx = new SqlConnection(connectionString);

        var evolve = new Evolve(cnx, msg => Console.WriteLine(msg))
        {

            EmbeddedResourceAssemblies = new[] { typeof(EducationDbContext).Assembly },
            EmbeddedResourceFilters = new[] { "TSWD.EducationManagement.EntityFrameworkCore.Migration" },

            IsEraseDisabled = true,
            MetadataTableName = "_EvolveMigrations"
        };

        evolve.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine("Database migration failed: " + ex.Message);
        throw;
    }
}

static void SeedPermissions(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var context = scope.ServiceProvider.GetRequiredService<EducationDbContext>();
        var definitionContext = new PermissionDefinitionContext();
        var provider = new EducationPermissionDefinitionProvider();
        provider.Define(definitionContext);

        foreach (var permission in definitionContext.GetPermissions())
        {
            if (!context.AppPermissions.Any(p => p.Name == permission.Name))
            {
                context.AppPermissions.Add(permission);
            }
        }

        context.SaveChanges();
    }
}
