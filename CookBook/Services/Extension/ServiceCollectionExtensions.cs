using CookBook.Composer;

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
            .AddControllers();
    }
}