using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Abstractions.Authentication;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.ValueTask;
using Soenneker.Docker.Registry.HttpClients.Abstract;
using Soenneker.Docker.Registry.OpenApiClientUtil.Abstract;
using Soenneker.Docker.Registry.OpenApiClient;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Docker.Registry.OpenApiClientUtil;

public sealed class DockerRegistryOpenApiClientUtil : IDockerRegistryOpenApiClientUtil
{
    private readonly AsyncSingleton<DockerRegistryOpenApiClient> _client;

    public DockerRegistryOpenApiClientUtil(IDockerRegistryOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<DockerRegistryOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var requestAdapter = new HttpClientRequestAdapter(new AnonymousAuthenticationProvider(), httpClient: httpClient);

            return new DockerRegistryOpenApiClient(requestAdapter);
        });
    }

    public ValueTask<DockerRegistryOpenApiClient> Get(CancellationToken cancellationToken = default)
    {
        return _client.Get(cancellationToken);
    }

    public void Dispose()
    {
        _client.Dispose();
    }

    public ValueTask DisposeAsync()
    {
        return _client.DisposeAsync();
    }
}
