using Soenneker.Docker.Registry.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Docker.Registry.OpenApiClientUtil.Abstract;
/// <summary>
/// Provides access to a cached, configured Docker Registry OpenAPI client.
/// </summary>
public interface IDockerRegistryOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured Docker Registry OpenAPI client for this utility's lifetime.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the cached Docker Registry OpenAPI client.</returns>
    ValueTask<DockerRegistryOpenApiClient> Get(CancellationToken cancellationToken = default);
}
