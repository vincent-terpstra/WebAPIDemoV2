using WebAPIDemoV2.API.Abstractions;
using WebAPIDemoV2.DataAccess.Interfaces;
using WebAPIDemoV2.Models;

namespace WebAPIDemoV2.API.Controllers;

public class TextsController : AbstractController<Text>
{
    public TextsController(IRepository<Text> repository) : base(repository)
    {
    }
}