using Library.Demo.Application;
using Library.Demo.Domain;
using Library.Demo.Infrastructure;
using Microsoft.OpenApi.Models;

namespace Library.Demo.Api;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddDatabase("User ID=libraray.demo.postgres;Password=libraray-docker;Host=localhost;Port=5432;Database=libraray.demo.db;");
        services.AddMediatR();
        services.AddApplicationService();
        services.AddSwaggerDocs();
        return services;
    }

    private static void AddApplicationService(this IServiceCollection services)
    {
        services.AddTransient<IBookService, BookService>();
        services.AddTransient<IBookRepository, BookRepository>();
    }

    private static void AddMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(CreateBookCommand).Assembly);
        });
    }

    private static void AddSwaggerDocs(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        // services.AddEndpointsApiExplorer(); // Not using minimal apis
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Version = "v1",
                Title = "Library Api",
                Description = "An API for the library app",
            });
        });
    }
}
