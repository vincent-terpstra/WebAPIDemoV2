using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.Models;

public sealed class User : BaseModel
{
    public string? FirstName { get; init; }

    public string? LastName { get; init; }
}