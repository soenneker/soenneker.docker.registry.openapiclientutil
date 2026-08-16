using Soenneker.Docker.Registry.OpenApiClientUtil.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Docker.Registry.OpenApiClientUtil.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class DockerRegistryOpenApiClientUtilTests : HostedUnitTest
{
    private readonly IDockerRegistryOpenApiClientUtil _openapiclientutil;

    public DockerRegistryOpenApiClientUtilTests(Host host) : base(host)
    {
        _openapiclientutil = Resolve<IDockerRegistryOpenApiClientUtil>(true);
    }

    [Test]
    public void Default()
    {

    }
}
