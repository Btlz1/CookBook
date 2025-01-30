using CookBook.Composer;
using FluentValidation;
using FluentValidation.AspNetCore;

public static class ServiceCollectionExtensions
{
    public static IMvcBuilder AddCustomServices(this IServiceCollection services, IConfiguration configuration)
    {
        return services
            .AddEndpointsApiExplorer()
            .AddConfiguredSwagger()
            .AddJwt()
            .AddAuth(configuration)
            .AddInfrastructure(configuration)
            .AddSwagger()
            .AddApplicationServices()
            .AddControllers()
            .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
            options.JsonSerializerOptions.MaxDepth = 32;  
        });
    }
}