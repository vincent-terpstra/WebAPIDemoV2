namespace WebAPIDemoV2.Tests.API;

public class IdentityTests : TestBase
{
    public IdentityTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    // [Fact]
    // public async Task Hello_Endpoint_Returns_World()
    // {
    //     // Arrange
    //     var client = CreateClient();
    //
    //     // Act
    //     var response = await client.GetAsync("Hello");
    //
    //     // Assert
    //     response.AssertSuccess();
    //     var content = await response.Content.ReadAsStringAsync();
    //     Assert.Equal("World", content);
    // }

    [Fact]
    public async Task Hello_Endpoint_Requires_Authorization()
    {
        // Arrange
        var client = CreateClient();
        
        // Act
        var response = await client.GetAsync("Hello");
        
        // Assert
        Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
    }

    [Fact]
    public async Task Register_Route_AddsUser()
    {
        // Arrange
        var client = CreateClient();

        var request = new
        {
            UserName = "TestUser",
            Password = "TestPassword@123",
            Email = "user@test.ca"
        };

        // Act
        var response = await client.PostAsJsonAsync("/register", request);

        // Assert
        response.AssertSuccess();
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }
}