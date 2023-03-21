using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using WebAPIDemoV2.DataAccess;

namespace WebAPIDemoV2.Tests.Controllers;

public class UserControllerTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public UserControllerTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient CreateClient()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<WebApiDemoDbContext>()!;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();
        return _factory.CreateClient(new WebApplicationFactoryClientOptions() {AllowAutoRedirect = false});
    }
        
    
    [Fact]
    public async Task ShouldGetAllUsers()
    {
        // Arrange
        var client = CreateClient();

        // Act
        var response = await client.GetAsync("/users");
        
        // Assert
        response.EnsureSuccessStatusCode();
        
    }
}