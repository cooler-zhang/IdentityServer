using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Coo.IdentityServer.IdentityAPI
{
    public class Config
    {
        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>()
            {
                new ApiResource("api1","Coo Web API")
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "client",
                    AllowedGrantTypes = {
                        GrantType.ClientCredentials
                    },
                    ClientSecrets = {
                        new Secret("secret".Sha256())
                    },
                    AllowedScopes = {
                        "api1"
                    }
                },
                new Client
                {
                    ClientId = "client.ro",
                    AllowedGrantTypes = {
                        GrantType.ResourceOwnerPassword
                    },
                    ClientSecrets = {
                        new Secret("secret.ro".Sha256())
                    },
                    AllowedScopes = {
                        "api1"
                    }
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }

        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser> {
                new TestUser(){
                    SubjectId = "1",
                    Username = "Cooler",
                    Password = "123456",
                    Claims = new List<Claim>()
                    {
                        new Claim("Email","cooler@test.com")
                    }
                },
                new TestUser(){
                    SubjectId = "2",
                    Username = "Test",
                    Password = "123456"
                }
            };
        }
    }
}
