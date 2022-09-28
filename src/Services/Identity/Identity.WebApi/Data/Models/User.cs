using Microsoft.AspNetCore.Identity;

namespace Identity.WebApi.Data.Models;

public class User : IdentityUser<int>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateOnly DateOfBirth { get; set; }
}