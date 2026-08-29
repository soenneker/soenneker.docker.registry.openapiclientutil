[![](https://img.shields.io/nuget/v/soenneker.docker.registry.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.docker.registry.openapiclientutil/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.docker.registry.openapiclientutil/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.docker.registry.openapiclientutil/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.docker.registry.openapiclientutil.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.docker.registry.openapiclientutil/)

# Soenneker.Docker.Registry.OpenApiClientUtil

Exposes a cached OpenAPI client instance.

## Install

```bash
dotnet add package Soenneker.Docker.Registry.OpenApiClientUtil
```

## Quick start

```csharp
using Soenneker.Docker.Registry.OpenApiClientUtil.Registrars;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
var result = services.AddDockerRegistryOpenApiClientUtilAsSingleton();
```

Adds `DockerRegistryOpenApiClientUtil` as a singleton service.

## What you get

- `IDockerRegistryOpenApiClientUtil` — Exposes a cached OpenAPI client instance.
- `DockerRegistryOpenApiClientUtilRegistrar` — Registers the OpenAPI client utility for dependency injection.

## API at a glance

| API | What it does | Result / important behavior |
| --- | --- | --- |
| `DockerRegistryOpenApiClientUtilRegistrar.AddDockerRegistryOpenApiClientUtilAsSingleton(services)` | Adds `DockerRegistryOpenApiClientUtil` as a singleton service. | The same service collection, so additional registrations can be chained. |
| `DockerRegistryOpenApiClientUtilRegistrar.AddDockerRegistryOpenApiClientUtilAsScoped(services)` | Adds `DockerRegistryOpenApiClientUtil` as a scoped service. | The same service collection, so additional registrations can be chained. |

## Practical notes

- Reuse the registered client instead of constructing one per operation.
- Dispose instances you own when their scope ends so held resources can be released.
