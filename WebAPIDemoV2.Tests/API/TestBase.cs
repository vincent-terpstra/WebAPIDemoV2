namespace WebAPIDemoV2.Tests.API;

public class TestBase : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public TestBase(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    protected HttpClient CreateClient()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<WebApiDemoDbContext>()!;
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

        var identityContext = scope.ServiceProvider.GetService < WebApiIdentityDbContext>()!;
            identityContext.Database.EnsureDeleted();
            identityContext.Database.EnsureCreated();
            
        return _factory.CreateClient(new WebApplicationFactoryClientOptions() {AllowAutoRedirect = false});
    }
}