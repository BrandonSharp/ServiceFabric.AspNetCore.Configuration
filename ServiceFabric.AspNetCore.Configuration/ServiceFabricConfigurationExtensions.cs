using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Text;

namespace ServiceFabric.AspNetCore.Configuration
{
    public static class ServiceFabricConfigurationExtensions {
        public static IWebHostBuilder AddServiceFabricConfiguration(this IWebHostBuilder builder, string packageName = null) {
            return builder.ConfigureAppConfiguration((context, config) => {
                config.Add(new ServiceFabricConfigurationSource(packageName ?? "Config"));
            });
        }
    }
}
