using Microsoft.Extensions.DependencyInjection;

namespace XrmBedrock.Extensions.Client.ManagedIdentity
{
    /// <summary>
    /// Extension methods for configuring Managed Identity authentication in XrmBedrock
    /// </summary>
    public static class ServiceCollectionExtensions 
    {
        /// <summary>
        /// Configures the service collection to use Managed Identity authentication for Dataverse
        /// </summary>
        /// <param name="services">The IServiceCollection to configure</param>
        /// <returns>The configured IServiceCollection</returns>
        public static IServiceCollection UseManagedIdentity(
            this IServiceCollection services)
        {
            services.AddScoped<IOrganizationServiceTokenProvider, ManagedIdentityTokenProvider>();
            return services;
        }
    }
}
