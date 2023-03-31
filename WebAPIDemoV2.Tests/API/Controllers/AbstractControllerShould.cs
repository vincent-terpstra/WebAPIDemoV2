namespace WebAPIDemoV2.Tests.API.Controllers;

public abstract class AbstractControllerShould<T> : IClassFixture<CustomWebApplicationFactory<Program>>
    where T: class, new()
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public AbstractControllerShould(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }
    
    protected HttpClient CreateClient()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<WebApiDemoDbContext>()!;
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return _factory.CreateClient(new WebApplicationFactoryClientOptions() {AllowAutoRedirect = false});
    }
}