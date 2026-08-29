using Soenneker.Docker.Registry.OpenApiClient;
using System;
using System.Threading;
using System.Threading.Tasks;
namespace Soenneker.Docker.Registry.OpenApiClientUtil.Abstract;
/// <summary>
/// Exposes a cached OpenAPI client instance.
/// </summary>
public interface IDockerRegistryOpenApiClientUtil: IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Returns the configured docker Registry OpenAPI Client used by the Docker Registry OpenAPI Client.
    /// </summary>
    /// <param name="cancellationToken">Token used to cancel the operation.</param>
    /// <returns>A task whose result is the requested docker Registry OpenAPI Client.</returns>
    ValueTask<DockerRegistryOpenApiClient> Get(CancellationToken cancellationToken = default);
}
