using WebAPIDemoV2.Domain.Entities;

namespace WebAPIDemoV2.Models;

public sealed class Text : BaseModel
{
    public required string Value { get; set; }
}