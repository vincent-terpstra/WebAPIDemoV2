using WebAPIDemoV2.Domain.Entities;
#nullable disable

namespace WebAPIDemoV2.Models;

public class User : BaseModel
{
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
}