using Microsoft.EntityFrameworkCore;
using PhoneNumbers.Application.IService;
using PhoneNumbers.Application.Service;
using PhoneNumbers.Core.IRepository;
using PhoneNumbers.Infrastructure.Data;
using PhoneNumbers.Infrastructure.Repository;

var builder = WebApplication.CreateBuilder(args);

// Configure services method
ConfigureServices(builder.Services, builder.Configuration);

var app = builder.Build();

// Ensure the database is created and seed data is applied
ApplyMigrationsAndSeedDatabase(app);

// Configure the HTTP request pipeline method
ConfigurePipeline(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration)
{
    // Add DbContext with an in-memory database
    services.AddDbContext<ApplicationDbContext>(options =>
        options.UseInMemoryDatabase("InMemoryDb"));

    // Register application services and repositories
    services.AddScoped<ICountryRepository, CountryRepository>();
    services.AddScoped<ICountryService, CountryService>();

    // Add controllers with JSON options
    services.AddControllers()
        .AddNewtonsoftJson(options =>
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

    // Add Swagger for API documentation
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen();
}

void ConfigurePipeline(WebApplication app)
{
    // Configure the HTTP request pipeline for development
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Add middleware
    app.UseHttpsRedirection();
    app.UseAuthorization();

    // Map controllers
    app.MapControllers();
}

void ApplyMigrationsAndSeedDatabase(WebApplication app)
{
    using (var scope = app.Services.CreateScope())
    {
        var services = scope.ServiceProvider;
        try
        {
            var context = services.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();
        }
        catch (Exception ex)
        {
            var logger = services.GetRequiredService<ILogger<Program>>();
            logger.LogError(ex, "An error occurred creating the database.");
        }
    }
}

public partial class Program { }
