using Microsoft.AspNetCore.Http;

namespace WebAPIDemoV2.Tests.API.Controllers;

public class UserControllerShould : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _factory;

    public UserControllerShould(CustomWebApplicationFactory<Program> factory)
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
    public async Task Return_List_Of_Users()
    {
        // Arrange
        var client = CreateClient();
        await CreateUserAsync(client);

        // Act
        var response = await client.GetAsync("/users");
        
        await response.AssertStatusSuccessAsync();
        List<User> users = (await response.Content.ReadFromJsonAsync<List<User>>())!;
        
        // Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.NotEmpty(users);
    }

    [Fact]
    public async Task Return_200_When_Found()
    {
        // Arrange
        var client = CreateClient();
        var user = await CreateUserAsync(client);

        //Act
        var response = await client.GetAsync($"/users/{user.Id}");
        
        await response.AssertStatusSuccessAsync();
        User getUser = (await response.Content.ReadFromJsonAsync<User>())!;
        
        //Assert
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        Assert.Equal(getUser.FirstName, user.FirstName);
        Assert.Equal(getUser.LastName, getUser.LastName);
    }

    [Fact]
    public async Task Return_404_When_NotFound()
    {
        //Arrange
        var client = CreateClient();
        
        //Act
        var response = await client.GetAsync($"/users/404");
        
        //Assert
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task Return_204_When_Deleted()
    {
        //Arrange
        var client = CreateClient();
        var user = await CreateUserAsync(client);

        //Act
        var delete = await client.DeleteAsync($"/users/{user.Id}");
        await delete.AssertStatusSuccessAsync();

        //Assert
        Assert.Equal(HttpStatusCode.NoContent, delete.StatusCode);
        
        //Assert that the item is deleted
        var get = await client.GetAsync($"/users/{user.Id}");
        Assert.Equal(HttpStatusCode.NotFound, get.StatusCode);
    }

    [Fact]
    public async Task Patch_Updates_Content()
    {
        //Arrange
        var client = CreateClient();
        var user = await CreateUserAsync(client);
        
        //Act
        var update = new { FirstName= "Updated Name" };
        var patch = await client.PatchAsJsonAsync($"/users/{user.Id}", update);
        
        //Assert
        await patch.AssertStatusSuccessAsync();
        User updated = (await patch.Content.ReadFromJsonAsync<User>())!;

        Assert.Equal(HttpStatusCode.OK, patch.StatusCode);
        Assert.Equal(update.FirstName, updated.FirstName);
        Assert.Equal(user.LastName, updated.LastName);
        
    }
    
    private async Task<User> CreateUserAsync(HttpClient client)
    {
        User user = CreateDefaultUser();

        var response = await client.PostAsJsonAsync("/users",user);
        response.EnsureSuccessStatusCode();
        return  (await response.Content.ReadFromJsonAsync<User>())!;
    }
    
    private User CreateDefaultUser(
            string firstname = "FirstName", 
            string lastname = "LastName"
        ) 
        => new() 
        {
            FirstName = firstname,
            LastName = lastname
        };
}