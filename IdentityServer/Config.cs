using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Security.Claims;

namespace IdentityServer
{
    public class Config
    {
        public static IEnumerable<Client> Clients => new Client[]
        {
            new Client
            {
                ClientId ="employeeClient",
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"employeeAPI"}
            },
            new Client
            {
                ClientId ="employee_user_Client",
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                ClientSecrets =
                {
                    new Secret("secret".Sha256())
                },
                AllowedScopes = {"employeeAPI"}
            },
            new Client
                {
                    ClientId = "angularclient",
                    ClientName = "Angular Client",
                    ClientUri = "https://localhost:4200",
                    RequireConsent = false,
                    AllowedGrantTypes = GrantTypes.Code,
                    RequirePkce = true,
                    RequireClientSecret = false,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris =
                    {
                        "https://localhost:4200/home",
                        "https://localhost:5003/callback.html"
                    },

                    PostLogoutRedirectUris = { "https://localhost:4200/login" },
                    AllowedCorsOrigins = { "https://localhost:4000" },

                    AllowedScopes = { "openid", "profile", "identity.api" , "employeeAPI" }
                }
        };
        public static IEnumerable<ApiScope> ApiScopes => new ApiScope[]
        {
            new ApiScope("employeeAPI", "Employee API")
        };
        public static IEnumerable<ApiResource> ApiResources => new ApiResource[] { 
        };
        public static IEnumerable<IdentityResource> IdentityResources => new IdentityResource[] {
            new IdentityResources.OpenId(),
            new IdentityResources.Email(),
            new IdentityResources.Profile(),
        };

        public static List<TestUser> TestUsers => new List<TestUser>() {
                new TestUser { SubjectId = "1", Username = "qaiser", Password = "qaiser2021",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Alice Smith"),
                        new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                        new Claim(JwtClaimTypes.Role, "admin")
                    }
                },
                new TestUser { SubjectId = "11", Username = "bob", Password = "bob",
                    Claims =
                    {
                        new Claim(JwtClaimTypes.Name, "Bob Smith"),
                        new Claim(JwtClaimTypes.Email, "BobSmith@email.com"),
                        new Claim(JwtClaimTypes.Role, "user")
                    }
                }
        };
    }
}
