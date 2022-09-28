namespace Ad.Domain.Models;

public class Country  : BaseModel
{
    public string Name { get; set; } = null!;

    public ICollection<Address> Addresses { get; set; } = null!;
}