using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;
using System;
using Microsoft.Identity.Client;
using System.Net.Http;
using System.Linq;

namespace XrmBedrock.Extensions.Client
{
    public class ClientCredentialsTokenProvider : BaseTokenProvider
    {
        private IConfiguration configuration;
        private readonly IHttpClientFactory httpClientFactory;
        private Lazy<Task<IConfidentialClientApplication>> app = null;

        public ClientCredentialsTokenProvider(IConfiguration configuration, IHttpClientFactory httpClientFactory)
        {
            this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
            this.httpClientFactory = httpClientFactory ?? throw new ArgumentNullException(nameof(httpClientFactory));
            app = new Lazy<Task<IConfidentialClientApplication>>(async () => {

                var tenantId = configuration.GetValue<string>("TenantId");
                if (string.IsNullOrEmpty(tenantId))
                {
                    var rsp = await httpClientFactory.CreateClient().GetAsync(
                            $"{new Uri(configuration.GetValue<string>("DataverseEnvironment")).GetLeftPart(UriPartial.Authority)}/api/data/v9.1/accounts");
                    var auth = rsp.Headers.GetValues("www-authenticate").FirstOrDefault();
                    var tenant = auth.Substring("Bearer ".Length).Split(',')
                        .Select(k => k.Trim().Split('='))
                        .ToDictionary(k => k[0], v => v[1]);


                    tenantId = new Uri(tenant["authorization_uri"]).AbsolutePath
                        .Split('/', StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
                }

                return ConfidentialClientApplicationBuilder.Create(configuration.GetValue<string>("DataverseClientId"))
                    .WithTenantId(tenantId)
                    .WithClientSecret(configuration.GetValue<string>("DataverseClientSecret"))
                    .Build();

            });
        }


        public override async Task<string> GetTokenAsync(string dataverseurl)
        {

            var client = await app.Value;
            var token = await client.AcquireTokenForClient(new[]
                { 
                    BuildScopeString(dataverseurl) 
                })
                .ExecuteAsync();
            return token.AccessToken;
        }
    }
}
