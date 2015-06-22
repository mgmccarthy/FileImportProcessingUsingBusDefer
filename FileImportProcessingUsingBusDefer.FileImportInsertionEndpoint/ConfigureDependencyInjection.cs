using FileImportProcessingUsingBusDefer.Data;
using NServiceBus;

namespace FileImportProcessingUsingBusDefer.FileImportInsertionEndpoint
{
    public class ConfigureDependencyInjection : INeedInitialization
    {
        public void Customize(BusConfiguration configuration)
        {
            configuration.RegisterComponents(reg => reg.ConfigureComponent<DataStore>(DependencyLifecycle.InstancePerCall));
        }
    }
}
