namespace Ad.Domain.Models;

public class City : BaseModel
{
    public string Name { get; set; } = null!;

    public ICollection<Address> Adresses { get; set; } = null!;
}