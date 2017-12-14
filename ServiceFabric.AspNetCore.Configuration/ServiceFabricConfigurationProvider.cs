using Microsoft.Extensions.Configuration;
using System.Fabric;

namespace ServiceFabric.AspNetCore.Configuration
{
    public class ServiceFabricConfigurationProvider : ConfigurationProvider {
        string packageName;
        CodePackageActivationContext context;

        public ServiceFabricConfigurationProvider(string packageName) {
            this.packageName = packageName;
            this.context = FabricRuntime.GetActivationContext();
            this.context.ConfigurationPackageModifiedEvent += (sender, e) => {
                this.LoadPackage(e.NewPackage, reload: true);
                this.OnReload();
            };
        }

        public override void Load() {
            var config = context.GetConfigurationPackageObject(this.packageName);
            LoadPackage(config);
        }

        void LoadPackage(ConfigurationPackage config, bool reload = false) {
            if (reload) {
                Data.Clear(); // Clear the old keys on reload
            }

            foreach (var section in config.Settings.Sections) {
                foreach (var param in section.Parameters) {
                    Data[$"{section.Name}:{param.Name}"] = param.IsEncrypted ? param.DecryptValue().ToUnsecureString() : param.Value;
                }
            }
        }
    }
}
