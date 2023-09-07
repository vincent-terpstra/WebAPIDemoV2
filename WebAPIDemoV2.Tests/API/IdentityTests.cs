namespace WebAPIDemoV2.Tests.API;

public class IdentityTests : TestBase
{
    public IdentityTests(CustomWebApplicationFactory<Program> factory) : base(factory)
    {
    }

    [Fact]
    public async Task Hello_Endpoint_Returns_World()
    {
        // Arrange
        var client = CreateClient();
        
        // Act
        var response = await client.GetAsync("Hello");

        // Assert
        response.AssertSuccess();
        var content = await response.Content.ReadAsStringAsync();
        Assert.Equal("World",content);
    }
}