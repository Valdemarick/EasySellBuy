namespace Ad.Domain.Models;

public class UserInfo : BaseModel
{
    public string UserName { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public ICollection<Ad> Ads { get; set; } = null!;
}