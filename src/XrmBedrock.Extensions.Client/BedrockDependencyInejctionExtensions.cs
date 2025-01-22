using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.PowerPlatform.Dataverse.Client;
using Microsoft.Xrm.Sdk;
using System;

namespace XrmBedrock.Extensions.Client
{
    public static class BedrockDependencyInejctionExtensions
    {

        public static IServiceCollection AddDataverse(this IServiceCollection services)
        {

            //  services.AddScoped(provider => provider.GetRequiredService<ILoggerFactory>().CreateLogger("DataverseService"));
            services.AddScoped(ServiceClientFactory);
            services.AddScoped<IOrganizationService, ServiceClient>(sp=>sp.GetService<ServiceClient>());
            services.AddScoped<IOrganizationServiceAsync, ServiceClient>(sp => sp.GetService<ServiceClient>());
            services.AddScoped<IOrganizationServiceAsync2, ServiceClient>(sp => sp.GetService<ServiceClient>());

            services.AddScoped<IOrganizationServiceTokenProvider, ClientCredentialsTokenProvider>();

            // services.AddScoped<IDataverseAccessObjectAsync, DataverseAccessObjectAsync>();
            // services.AddScoped<IExtendedTracingService, ExtendedTracingService>();
            //  services.AddScoped<ILoggingComponent, LoggingComponent>();
            //  services.AddScoped<IDataverseCustomApiService, DataverseCustomApiService>();

            return services;
        }


        private static ServiceClient ServiceClientFactory(IServiceProvider provider)
        {
            var configuration = provider.GetRequiredService<IOptions<OrganizationServiceOptions>>();
            var logger = provider.GetRequiredService<ILogger>();

            var dataverseUrl = configuration.Value.DataverseUrl;
            var cache = provider.GetService<IMemoryCache>();

            var client = new ServiceClient(instanceUrl: new Uri(dataverseUrl), tokenProviderFunction: url=> provider.GetService<IOrganizationServiceTokenProvider>().GetTokenAsync(url));
            client.EnableAffinityCookie = configuration.Value.EnableAffinityCookie;
            return client;
        }

          

       
    }
}
