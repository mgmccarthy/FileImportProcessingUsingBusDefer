using FileImportProcessingUsingBusDefer.Data;
using NServiceBus;
using NServiceBus.Config;

namespace FileImportProcessingUsingBusDefer.FileImportInsertionEndpoint
{
    public class EnsureTablesAreCreatedWhenConfiguringEndpoint : IWantToRunWhenConfigurationIsComplete
    {
        public void Run(Configure config)
        {
            using (var context = new FileImportContext())
                context.Database.Initialize(false);
        }
    }
}
