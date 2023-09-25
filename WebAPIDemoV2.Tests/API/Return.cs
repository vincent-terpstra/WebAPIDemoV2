using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.Tests.API;

public abstract class Return<T> : IClassFixture<CustomWebApplicationFactory<Program>>
    where T: BaseModel
{
    private readonly CustomWebApplicationFactory<Program> _factory;
    private readonly string _baseRoute;

    
    public Return(CustomWebApplicationFactory<Program> factory, string baseRoute)
    {
        _factory = factory;
        _baseRoute = baseRoute;
    }
    
    protected HttpClient CreateClient()
    {
        using var scope = _factory.Services.CreateScope();
        var context = scope.ServiceProvider.GetService<WebApiDemoDbContext>()!;
        context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        return _factory.CreateClient(new WebApplicationFactoryClientOptions() {AllowAutoRedirect = false});
    }
    
    [Fact]
    public async Task _200_Get_All()
    {
        // Arrange
        var client = CreateClient();
        await PostValueAsync(client);

        // Act
        var response = await client.GetAsync(_baseRoute);
        
        await response.AssertStatusSuccessAsync();
        var list = (await response.Content.ReadFromJsonAsync<List<T>>())!;
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(list);
    }

    [Fact]
    public async Task _200_When_Found()
    {
        // Arrange
        var client = CreateClient();
        var posted = await PostValueAsync(client);

        // Act
        var response = await client.GetAsync($"{_baseRoute}/{posted.Id}");
        
        await response.AssertStatusSuccessAsync();
        T value = (await response.Content.ReadFromJsonAsync<T>())!;
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        foreach (var property in typeof(T).GetProperties())
        {
            Assert.Equal(
                property.GetValue(posted), 
                property.GetValue(value));
            
        }
        //Assert.Equal(getUser.FirstName, user.FirstName);
        //Assert.Equal(getUser.LastName, getUser.LastName);
    }

    [Fact]
    public async Task _404_When_NotFound()
    {
        // Arrange
        var client = CreateClient();
        
        // Act
        var response = await client.GetAsync($"{_baseRoute}/404");
        
        // Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task _204_When_Deleted()
    {
        // Arrange
        var client = CreateClient();
        var posted = await PostValueAsync(client);

        // Act
        var delete = await client.DeleteAsync($"{_baseRoute}/{posted.Id}");
        await delete.AssertStatusSuccessAsync();

        // Assert
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);
        
        // Assert that the item is deleted
        var get = await client.GetAsync($"{_baseRoute}/{posted.Id}");
        Assert.Equal(HttpStatusCode.NotFound, get.StatusCode);
    }
    
    protected virtual async Task<T> PostValueAsync(HttpClient client)
    {
        T value = CreateDefaultValue();

        var response = await client.PostAsJsonAsync(_baseRoute, value);
        response.EnsureSuccessStatusCode();
        return  (await response.Content.ReadFromJsonAsync<T>())!;
    }

    protected abstract T CreateDefaultValue();
}