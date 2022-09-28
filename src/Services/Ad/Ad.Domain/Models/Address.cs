namespace Ad.Domain.Models;

public class Address : BaseModel
{
    public int CityId { get; set; }
    public int CountryId { get; set; }

    public City City { get; set; } = null!;
    public Country Country { get; set; } = null!;
}