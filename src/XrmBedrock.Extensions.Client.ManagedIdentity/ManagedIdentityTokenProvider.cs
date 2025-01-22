using Azure.Core;
using Azure.Identity;
using System;
using System.Threading;
using System.Threading.Tasks;
using XrmBedrock.Extensions.Client;

namespace XrmBedrock.Extensions.Client.ManagedIdentity
{
    /// <summary>
    /// Provides token acquisition using Azure Managed Identity for Dataverse authentication
    /// </summary>
    public class ManagedIdentityTokenProvider : IOrganizationServiceTokenProvider
    {
        private readonly DefaultAzureCredential _credential;
        
        /// <summary>
        /// Initializes a new instance of the ManagedIdentityTokenProvider
        /// </summary>
        public ManagedIdentityTokenProvider()
        {
            _credential = new DefaultAzureCredential();
        }

        /// <summary>
        /// Gets an access token for the specified Dataverse resource
        /// </summary>
        /// <param name="resourceUri">The Dataverse resource URI</param>
        /// <returns>An access token for the specified resource</returns>
        public async Task<string> GetTokenAsync(string resourceUri)
        {
            var scope = $"{GetCoreUrl(resourceUri)}/.default";
            var token = await _credential.GetTokenAsync(
                new TokenRequestContext(new[] { scope }), 
                CancellationToken.None);
            return token.Token;
        }

        private static string GetCoreUrl(string url)
        {
            var uri = new Uri(url);
            return $"{uri.Scheme}://{uri.Host}";
        }
    }
}
