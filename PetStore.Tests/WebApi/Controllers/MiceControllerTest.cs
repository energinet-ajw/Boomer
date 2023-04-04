using System.Net;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit;

namespace PetStore.Tests.WebApi.Controllers;

public class MiceControllerTest : IClassFixture<WebApplicationFactory<PetStore.Api.Program>>
{
    private readonly WebApplicationFactory<PetStore.Api.Program> _factory;

    public MiceControllerTest(WebApplicationFactory<PetStore.Api.Program> factory)
    {
        _factory = factory;
    }

    [Fact]
    public async Task GetAsync_ToEnsureGetMouseEndpointExists()
    {
        // Arrange
        var client = _factory.CreateClient(new WebApplicationFactoryClientOptions());

        // Act
        var actual = (await client.GetAsync("api/v1/mice/b4bbbe0a-bebf-441d-b603-f2c02e40c69f"));

        // Assert
        Assert.Equal(HttpStatusCode.BadRequest, actual.StatusCode);
    }
}