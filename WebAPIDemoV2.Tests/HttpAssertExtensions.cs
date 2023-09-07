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

    public static void AssertSuccess(this HttpResponseMessage responseMessage)
    {
        if (responseMessage.IsSuccessStatusCode is false)
        {
            var message = responseMessage.Content.ReadAsStringAsync().Result;
            Assert.Fail(message);
        }
    }
}