using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Soenneker.Extensions.Configuration;
using Soenneker.Extensions.ValueTask;
using Soenneker.Docker.Registry.HttpClients.Abstract;
using Soenneker.Docker.Registry.OpenApiClientUtil.Abstract;
using Soenneker.Docker.Registry.OpenApiClient;
using Soenneker.Kiota.GenericAuthenticationProvider;
using Soenneker.Utils.AsyncSingleton;

namespace Soenneker.Docker.Registry.OpenApiClientUtil;

///<inheritdoc cref="IDockerRegistryOpenApiClientUtil"/>
public sealed class DockerRegistryOpenApiClientUtil : IDockerRegistryOpenApiClientUtil
{
    private readonly AsyncSingleton<DockerRegistryOpenApiClient> _client;

    public DockerRegistryOpenApiClientUtil(IDockerRegistryOpenApiHttpClient httpClientUtil, IConfiguration configuration)
    {
        _client = new AsyncSingleton<DockerRegistryOpenApiClient>(async token =>
        {
            HttpClient httpClient = await httpClientUtil.Get(token).NoSync();

            var apiKey = configuration.GetValueStrict<string>("DockerRegistry:AccessToken");
            string authHeaderName = configuration["Registry:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = configuration["Registry:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", apiKey, StringComparison.Ordinal);

            var requestAdapter = new HttpClientRequestAdapter(new GenericAuthenticationProvider(headerName: authHeaderName, headerValue: authHeaderValue),
                httpClient: httpClient);

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
