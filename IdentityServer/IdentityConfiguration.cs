using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer
{
    public static class IdentityConfiguration
    {
        public static string Admin = "Admin";
        public static string Customer = "Customer";

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            yield return new IdentityResources.OpenId();
            yield return new IdentityResources.Email();
            yield return new IdentityResources.Profile();
        }
        public static IEnumerable<ApiScope> GetApiScopes()
        {
            yield return new ApiScope("fact", "Fact Server");
            yield return new ApiScope("read", "read data");
            yield return new ApiScope("delete", "delete data");
            yield return new ApiScope("write", "write data");
        }
        public static IEnumerable<Client> GetClients()
        {
            yield return new Client
            {
                ClientId = "client",
                ClientSecrets = { new Secret("secret".ToSha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = {"read", "write", "profile"},
            };
            yield return new Client
            {
                ClientId = "fact",
                ClientSecrets = { new Secret("secret".ToSha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                RedirectUris= { "https://localhost:7086/signin-oidc" },
                PostLogoutRedirectUris = { "https://localhost:7086/signout-callback-oidc" },
                AllowedScopes = 
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "fact"
                },
            };
        }
    }
}
