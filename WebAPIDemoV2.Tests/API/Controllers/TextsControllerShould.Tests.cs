namespace WebAPIDemoV2.Tests.API.Controllers;

public class TextsControllerShould : Return<Text>
{
    public TextsControllerShould(CustomWebApplicationFactory<Program> factory) : base(factory, "/texts")
    {
    }

    protected override Text CreateDefaultValue()
    {
        return new Text()
        {
            Value = "Test Text"
        };
    }
}