using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApi.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Add API explorer and Swagger support
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Web API", Version = "v1" });
    // Add the following line to resolve the CORS error
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Name = "Authorization",
        Description = "JWT Authorization header using the Bearer scheme"
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

// Add DbContext for WebApiDbContext using SQLite database
builder.Services.AddDbContext<WebApiDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("WebApiDbContext")));

// Build the WebApplication
var app = builder.Build();

// Use Swagger for API documentation in development environment
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web API v1");
    });
}

// Add the following line to resolve the CORS error
app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

// Start the application
app.Run();
