using Microsoft.Extensions.Configuration;

namespace ServiceFabric.AspNetCore.Configuration
{
    public class ServiceFabricConfigurationSource : IConfigurationSource {
        public string PackageName { get; set; }

        public ServiceFabricConfigurationSource(string packageName) {
            PackageName = packageName;
        }

        public IConfigurationProvider Build(IConfigurationBuilder builder) {
            return new ServiceFabricConfigurationProvider(PackageName);
        }
    }
}