using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coo.IdentityServer.Console
{
    using System;

    class Program
    {
        static void Main(string[] args)
        {
            var tokenResponse = RequestTokenAsync().Result;
            var result = CallServiceAsync(tokenResponse.AccessToken).Result;
            Console.WriteLine(result);
            Console.ReadLine();
        }

        public static async Task<string> CallServiceAsync(string token)
        {
            var baseAddress = Constants.SampleApi;

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            client.SetBearerToken(token);
            return await client.GetStringAsync("api/values");
        }

        public static async Task<TokenResponse> RequestTokenAsync()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(Constants.Authority);
            if (disco.IsError) throw new Exception(disco.Error);

            //var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            //{
            //    Address = disco.TokenEndpoint,

            //    ClientId = "client",
            //    ClientSecret = "secret",
            //    Scope = "api1"
            //});

            var response = await client.RequestPasswordTokenAsync(new PasswordTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "client.ro",
                ClientSecret = "secret.ro",
                Scope = "api1",

                UserName = "Cooler",
                Password = "123456"
            });

            if (response.IsError) throw new Exception(response.Error);
            return response;
        }
    }

    public class Constants
    {
        public const string Authority = "http://localhost:61868/";
        //public const string Authority = "https://local.identityserver.io";

        public const string SampleApi = "http://localhost:61874/";
        //public const string SampleApi = "https://api.identityserver.io";
    }
}
