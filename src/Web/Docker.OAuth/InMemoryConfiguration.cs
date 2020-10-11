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
            new ApiResource("product", "Product")
        };

        public static IEnumerable<ApiScope> ApiScopes() => new[]
        {
            new ApiScope("product.read", "Read Product"),
            new ApiScope("product.write", "Write Product")
        };

        public static IEnumerable<Client> ApiClients() => new[]
        {
            new Client()
            {
                ClientId = "testclient",
                ClientSecrets = new [] { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedScopes = new [] { "product.read", "product.write" },
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
