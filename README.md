[![](https://img.shields.io/nuget/v/soenneker.docker.registry.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.docker.registry.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.docker.registry.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.docker.registry.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.docker.registry.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.docker.registry.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.docker.registry.openapiclientutil/codeql.yml?label=CodeQL&style=for-the-badge)](https://github.com/soenneker/soenneker.docker.registry.openapiclientutil/actions/workflows/codeql.yml)

# Soenneker.Docker.Registry.OpenApiClientUtil

Provides a dependency-injection-friendly, cached instance of the generated Docker Registry API client.

## Installation

```bash
dotnet add package Soenneker.Docker.Registry.OpenApiClientUtil
```

## Configuration

```json
{
  "DockerRegistry": {
    "AccessToken": "your-registry-access-token"
  }
}
```

The token must already have the repository scopes required by the operations you call. Keep it in a secret provider rather than source control.

## Registration

```csharp
using Soenneker.Docker.Registry.OpenApiClientUtil.Registrars;

services.AddDockerRegistryOpenApiClientUtilAsScoped();
```

The scoped registration creates one cached generated client per dependency-injection scope while retaining the underlying Registry HTTP client provider as a singleton. Disposing the util at the end of a scope does not destroy that shared transport.

Use `AddDockerRegistryOpenApiClientUtilAsSingleton()` when the generated-client holder should also live for the application lifetime.

## Usage

```csharp
using Soenneker.Docker.Registry.OpenApiClient;
using Soenneker.Docker.Registry.OpenApiClient.Models;
using Soenneker.Docker.Registry.OpenApiClientUtil.Abstract;

public sealed class ManifestReader(IDockerRegistryOpenApiClientUtil clientUtil)
{
    public async Task<GetImageManifest200DockerDistributionManifestV2JsonResponse?> Get(
        string repository,
        string reference,
        CancellationToken cancellationToken)
    {
        DockerRegistryOpenApiClient client = await clientUtil.Get(cancellationToken);

        return await client.V2[repository]
                           .Manifests[reference]
                           .GetAsync(cancellationToken: cancellationToken);
    }
}
```

The repository indexer accepts the repository name, and the manifest indexer accepts a tag or digest. `Get` returns the same generated client for the lifetime of the util.

This utility applies a configured bearer token; it does not process Registry authentication challenges, obtain repository-scoped tokens, or refresh them. Optional transport overrides use `Registry:ClientBaseUrl`, `Registry:AuthHeaderName`, and `Registry:AuthHeaderValueTemplate` and must be treated as trusted configuration.

The generated blob `GetAsync` currently returns no response body, so use the underlying transport package or another raw HTTP client when downloading blob content. Generated operations also lack Registry-specific typed error mappings; handle Kiota request and transport failures at the application boundary.
