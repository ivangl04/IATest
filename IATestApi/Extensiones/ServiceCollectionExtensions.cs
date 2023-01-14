namespace IATest.Extensiones
{
    using AutoMapper;
    using FluentValidation;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.JsonPatch;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.DependencyInjection;
    using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
    using IATest.Controllers.Mappers;
    using IATest.Models.Data.DbContext;
    using IATest.Models.Resource;
    using IATest.Repository.Solicitud;
    using IATest.Services.Mappers;
    using IATest.Services.Solicitud;
    using IATest.Validator;

    public static class ServiceCollectionExtensions
    {
        public static void AddIATestDbContext (this IServiceCollection collection, string connectionString)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 0));
            collection.AddDbContext<IIATestDbContext, IATestDbContext>(options => options
            .UseMySql(connectionString, serverVersion, b => b.SchemaBehavior(MySqlSchemaBehavior.Translate, (schema, entity) => $"{schema ?? "dbo"}_{entity}"))
            .LogTo(Console.WriteLine, LogLevel.Information)
            .EnableSensitiveDataLogging()
            .EnableDetailedErrors());
        }

        // AutoBuild Db config
        public static void BuidDataBase(this WebApplication app)
        {
            using var scope = app.Services.CreateScope();
            var dataContext = scope.ServiceProvider.GetRequiredService<IATestDbContext>();
            dataContext.Database.Migrate();
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<ISolicitudRepository, SolicitudRepository>();
            services.AddScoped<ISolicitudService, SolicitudService>();
            services.AddScoped<IValidator<SolicitudPost>, SolicitudPostValidator>();
            services.AddScoped<IValidator<JsonPatchDocument<SolicitudPatch>>, SolicitudPatchValidator>();
            services.AddScoped<IValidator<Solicitud>, SolicitudPutValidator>();

            // Configuracion de Auto Mapper 
            var mappingConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new ControllerMappingProfile());
                mc.AddProfile(new ServiceMappingProfile());
            });
            var mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
    }
}
