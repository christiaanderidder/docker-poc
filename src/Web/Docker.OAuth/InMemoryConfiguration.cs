using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace Docker.OAuth
{
    public class InMemoryConfiguration
    {
        public static IEnumerable<ApiResource> ApiResources() => new[]
        {
            new ApiResource("products", "Products")
        };

        public static IEnumerable<ApiScope> ApiScopes() => new[]
        {
            new ApiScope("products.read", "Read Products"),
            new ApiScope("products.write", "Write Products")
        };

        public static IEnumerable<Client> ApiClients() => new[]
        {
            new Client()
            {
                ClientId = "testclient",
                ClientSecrets = new [] { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = new [] { "products.read", "products.write" },
            }
        };

        public static IEnumerable<TestUser> Users() => new[]
        {
            new TestUser()
            {
                SubjectId = "1",
                Username = "test@user.com",
                Password = "password"
            }
        };
    }
}
