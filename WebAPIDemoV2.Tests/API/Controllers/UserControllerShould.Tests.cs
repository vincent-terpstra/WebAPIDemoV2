namespace WebAPIDemoV2.Tests.API.Controllers;

public class UserControllerShould : Return<User>
{
    public UserControllerShould(CustomWebApplicationFactory<Program> factory) : base(factory, "/users")
    {
    }
    
    [Fact]
    public async Task Return_200_On_Patch()
    {
        // Arrange
        var client = CreateClient();
        var user = await CreateUserAsync(client);
        
        // Act
        var update = new { FirstName = "Updated Name" };
        var patch = await client.PatchAsJsonAsync($"/users/{user.Id}", update);
        
        // Assert
        await patch.AssertStatusSuccessAsync();
        User updated = (await patch.Content.ReadFromJsonAsync<User>())!;

        Assert.Equal(HttpStatusCode.OK, patch.StatusCode);
        Assert.Equal(update.FirstName, updated.FirstName);
        Assert.Equal(user.LastName, updated.LastName);
        
    }
    
    private async Task<User> CreateUserAsync(HttpClient client)
    {
        User user = CreateDefaultValue();

        var response = await client.PostAsJsonAsync("/users",user);
        response.EnsureSuccessStatusCode();
        return  (await response.Content.ReadFromJsonAsync<User>())!;
    }
    
    protected override User CreateDefaultValue() 
        => new() 
        {
            FirstName = "firstname",
            LastName = "lastname"
        };
}