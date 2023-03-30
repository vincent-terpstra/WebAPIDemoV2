namespace WebAPIDemoV2.Tests;

public static class HttpAssertExtensions
{
    public static async Task AssertStatusSuccessAsync(this HttpResponseMessage response)
    {
        if (!response.IsSuccessStatusCode)
        {
            Assert.Fail(await response.Content.ReadAsStringAsync());
        }
    }
}