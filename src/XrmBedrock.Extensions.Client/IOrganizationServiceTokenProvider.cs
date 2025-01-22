using System.Threading.Tasks;

namespace XrmBedrock.Extensions.Client
{
    public interface IOrganizationServiceTokenProvider
    {
        Task<string> GetTokenAsync(string dataverseUrl);
    }
}
