using IdentityServer4;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System.Collections.Generic;
using System.Security.Claims;

namespace AuthServer.Configuration
{
    public class Config
    {
        public static IEnumerable<IdentityResource> GetIdentityResources() =>
              new List<IdentityResource>
              {
                  new IdentityResources.OpenId(),
                  new IdentityResources.Profile(),
                  new IdentityResources.Address(),
                  new IdentityResource("roles", "User role(s)", new List<string> { "role" })
              };


        public static List<TestUser> GetUsers() =>
            new List<TestUser>
              {
                  new TestUser
                  {
                      SubjectId = "a9ea0f25-b964-409f-bcce-c923266249b4",
                      Username = "qaiser",
                      Password = "qaiser",
                      Claims = new List<Claim>
                      {
                          new Claim("given_name", "qaiser"),
                          new Claim("family_name", "imam"),
                          new Claim("address", "Sunny Street 4"),
                          new Claim("role", "Admin")
                      }
                  },
                  new TestUser
                  {
                      SubjectId = "c95ddb8c-79ec-488a-a485-fe57a1462340",
                      Username = "manzer",
                      Password = "manzer",
                      Claims = new List<Claim>
                      {
                          new Claim("given_name", "manzer"),
                          new Claim("family_name", "imam"),
                          new Claim("address", "Sunny Street 4"),
                          new Claim("role", "Employee")
                      }
                  }
              };

        public static IEnumerable<Client> GetClients() =>
            new List<Client>
            {
               new Client
               {
                    ClientId = "human-resource",
                    ClientSecrets = new [] { new Secret("humanresourcesecret".Sha512()) },
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, "employeeApi" }
                },
                new Client
                {
                    ClientName = "Employee Client",
                    ClientId = "employee-client",
                    AllowedGrantTypes = GrantTypes.Hybrid,
                    RedirectUris = new List<string>{ "https://localhost:5010/signin-oidc" },
                    RequirePkce = false,
                    AllowedScopes = { IdentityServerConstants.StandardScopes.OpenId, IdentityServerConstants.StandardScopes.Profile, IdentityServerConstants.StandardScopes.Address, "roles", "employeeApi" },
                    ClientSecrets = { new Secret("EmployeeSecret".Sha512()) },
                    PostLogoutRedirectUris = new List<string> { "https://localhost:5010/signout-callback-oidc" }
                },
                new Client
                {
                    ClientName = "Angular-Client",
                    ClientId = "angular-client",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>{ "http://localhost:4200/signin-callback", "http://localhost:4200/assets/silent-callback.html" },
                    RequirePkce = true,
                    AllowAccessTokensViaBrowser = true,
                    AllowedScopes =
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "employeeApi"
                    },
                    AllowedCorsOrigins = { "http://localhost:4200" },
                    RequireClientSecret = false,
                    PostLogoutRedirectUris = new List<string> { "http://localhost:4200/signout-callback" },
                    RequireConsent = false,
                    AccessTokenLifetime = 600
                }
            };

        public static IEnumerable<ApiResource> GetApiResources() =>
            new List<ApiResource>
            {
                new ApiResource("employeeApi", "Employee API")
                {
                    Scopes = { "employeeApi" },
                    UserClaims = new List<string>{"roles"}
                }
            };
        public static IEnumerable<ApiScope> GetApiScopes() =>
            new List<ApiScope> { new ApiScope("employeeApi", "Employee API") };
    }
}
