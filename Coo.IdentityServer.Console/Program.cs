using IdentityModel.Client;
using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Coo.IdentityServer.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tokenResponse = RequestTokenAsync().Result;
            CallServiceAsync(tokenResponse.AccessToken).Wait();
        }

        public static async Task CallServiceAsync(string token)
        {
            var baseAddress = Constants.SampleApi;

            var client = new HttpClient
            {
                BaseAddress = new Uri(baseAddress)
            };

            client.SetBearerToken(token);
            var response = await client.GetStringAsync("api/values");
        }


        public static async Task<TokenResponse> RequestTokenAsync()
        {
            var client = new HttpClient();

            var disco = await client.GetDiscoveryDocumentAsync(Constants.Authority);
            if (disco.IsError) throw new Exception(disco.Error);

            var response = await client.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest
            {
                Address = disco.TokenEndpoint,

                ClientId = "WebAPIClient",
                ClientSecret = "WebAPISecret"
            });

            if (response.IsError) throw new Exception(response.Error);
            return response;
        }
    }

    public class Constants
    {
        public const string Authority = "http://localhost:61868";
        //public const string Authority = "https://local.identityserver.io";

        public const string SampleApi = "http://localhost:61874";
        //public const string SampleApi = "https://api.identityserver.io";
    }
}
