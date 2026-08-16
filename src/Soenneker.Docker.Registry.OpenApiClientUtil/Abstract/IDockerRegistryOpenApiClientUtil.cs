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
    ValueTask<DockerRegistryOpenApiClient> Get(CancellationToken cancellationToken = default);
}
