using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace Identity.WebApi;

public static class IdentityConfig
{
    public static IEnumerable<IdentityResource> GetIdentityResources() => new List<IdentityResource>()
    {
        new IdentityResources.OpenId(),
        new IdentityResources.Profile()
    };

    public static List<TestUser> GetTestUsers() => new List<TestUser>()
    {
        new TestUser()
        {
            SubjectId = "1",
            Username = "Jack_1977",
            Password = "jackjackovich1977",
            Claims = new List<Claim>()
            {
                 new Claim(JwtClaimTypes.GivenName, "Jack"),
                 new Claim(JwtClaimTypes.FamilyName, "Phobes")
            }
        },
        new TestUser()
        {
            SubjectId = "2",
            Username = "bugzilla1",
            Password = "bugzilla777",
            Claims = new List<Claim>()
            {
                 new Claim(JwtClaimTypes.GivenName, "John"),
                 new Claim(JwtClaimTypes.FamilyName, "Holms")
            }
        }
    };

    public static IEnumerable<Client> GetClients() => new[]
    {
        new Client
        {
            ClientId = "Api",
            ClientName = "ClientApi",
            AllowAccessTokensViaBrowser = true,
            ClientSecrets = new [] { new Secret("easysellbuysecret".Sha512()) },
            AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
            AllowedScopes = { "Bag.WebApi", "Ad.WebApi" }
        },
        new Client
        {
            ClientId = "Postman",
            ClientSecrets = new [] { new Secret("postmansecret".Sha512()) },
            AllowedGrantTypes = GrantTypes.ClientCredentials,
            AllowedScopes = { "Ad.WebApi" }
        }
    };

    public static IEnumerable<ApiResource> GetApiResources() => new List<ApiResource>()
    {
        new ApiResource("Ad.WebApi", "Ad.WebApi") { Scopes = { "Ad.WebApi" } },
        new ApiResource("Bag.WebApi", "Bag.WebApi")
    };

    public static IEnumerable<ApiScope> GetApiScopes() => new List<ApiScope>()
    {
        new ApiScope("Ad.WebApi", "Ad.WebApi"),
        new ApiScope("Bag.WebApi", "Bag.WebApi")
    };
}