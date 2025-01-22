using System.Threading.Tasks;
using System;

namespace XrmBedrock.Extensions.Client
{
    public abstract class BaseTokenProvider : IOrganizationServiceTokenProvider
    {
        public abstract Task<string> GetTokenAsync(string dataverseUrl);

        protected string BuildScopeString(string dataverseUrl)
        {
            return $"{GetCoreUrl(dataverseUrl)}/.default";
        }

        protected string GetCoreUrl(string url)
        {
            var uri = new Uri(url);
            return $"{uri.Scheme}://{uri.Host}";
        }

    }
}
