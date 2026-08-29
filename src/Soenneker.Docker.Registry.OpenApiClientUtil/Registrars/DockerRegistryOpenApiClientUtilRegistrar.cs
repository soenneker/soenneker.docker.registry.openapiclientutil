using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Docker.Registry.HttpClients.Registrars;
using Soenneker.Docker.Registry.OpenApiClientUtil.Abstract;

namespace Soenneker.Docker.Registry.OpenApiClientUtil.Registrars;

/// <summary>
/// Registers the OpenAPI client utility for dependency injection.
/// </summary>
public static class DockerRegistryOpenApiClientUtilRegistrar
{
    /// <summary>
    /// Adds <see cref="DockerRegistryOpenApiClientUtil"/> as a singleton service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddDockerRegistryOpenApiClientUtilAsSingleton(this IServiceCollection services)
    {
        services.AddDockerRegistryOpenApiHttpClientAsSingleton()
                .TryAddSingleton<IDockerRegistryOpenApiClientUtil, DockerRegistryOpenApiClientUtil>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="DockerRegistryOpenApiClientUtil"/> as a scoped service. <para/>
    /// </summary>
    /// <param name="services">Service collection that receives the registration.</param>
    /// <returns>The same service collection, so additional registrations can be chained.</returns>
    public static IServiceCollection AddDockerRegistryOpenApiClientUtilAsScoped(this IServiceCollection services)
    {
        services.AddDockerRegistryOpenApiHttpClientAsSingleton()
                .TryAddScoped<IDockerRegistryOpenApiClientUtil, DockerRegistryOpenApiClientUtil>();

        return services;
    }
}
