using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;

namespace Hrms.IntegrationTests;

public class SmokeTests(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
{
    [Fact]
    public async Task Health_endpoint_returns_ok()
    {
        var client = factory.CreateClient();

        var response = await client.GetAsync("/health", TestContext.Current.CancellationToken);

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task Modules_endpoint_lists_all_five_modules()
    {
        var client = factory.CreateClient();

        var modules = await client.GetFromJsonAsync<List<ModuleInfo>>("/api/modules", TestContext.Current.CancellationToken);

        Assert.NotNull(modules);
        Assert.Equal(["foundation", "recruitment", "personnel", "training", "cnb"], modules.Select(m => m.Key));
    }

    private sealed record ModuleInfo(string Key, string Name);
}
