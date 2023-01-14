using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using IATest.Configuration;
using IATest.Extensiones;
using IATest.Models.Data.DbContext;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "1.0.0",
        Title = "IA Test",
        Description = "Web API that contains the functionality related to IA Test",
        Contact = new OpenApiContact()
        {
            Email = "ivangarridolugo@gmail.com",
            Name = "Ivan Garrido"
        }
    });
});

//Add DB Connection
builder.Services.AddIATestDbContext(builder.Configuration.GetConnectionString("IATestDatabase"));

// Add config of scoped services and Mappers.
builder.Services.AddServices();

// Add Cors
var corsConfiguration = builder.Configuration.GetSection("Cors").Get<CorsConfiguration>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(
        name: corsConfiguration.Name,
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddControllers()
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        options.SerializerSettings.Converters.Add(new StringEnumConverter());
    });

builder.Services.AddMvc(options =>
{
    options.EnableEndpointRouting = false;
    options.ReturnHttpNotAcceptable = true;
});

builder.Services.AddSwaggerGenNewtonsoftSupport();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var applicationDbContext = scope.ServiceProvider.GetRequiredService<IATestDbContext>();
    applicationDbContext.Database.EnsureCreated();
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// app.UseAuthorization();

app.MapControllers();

app.UseCors(corsConfiguration.Name);

app.Run();
