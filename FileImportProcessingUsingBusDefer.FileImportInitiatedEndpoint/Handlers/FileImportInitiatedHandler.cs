using System;
using System.Linq;
using FileImportProcessingUsingBusDefer.Data;
using FileImportProcessingUsingBusDefer.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace FileImportProcessingUsingBusDefer.FileImportInitiatedEndpoint.Handlers
{
    public class FileImportInitiatedHandler : IHandleMessages<FileImportInitiated>
    {
        private readonly IBus bus;
        private readonly IDataStore dataStore;
        private static readonly ILog Log = LogManager.GetLogger(typeof(FileImportInitiatedHandler));

        public FileImportInitiatedHandler(IBus bus, IDataStore dataStore)
        {
            this.bus = bus;
            this.dataStore = dataStore;
        }

        public void Handle(FileImportInitiated message)
        {
            int rowsSucceeded;
            int rowsFailed;
            using (var session = dataStore.OpenSession())
            {
                rowsSucceeded = session.Query<FileImport>().Where(x => x.ImportId == message.ImportId).Count(x => x.Successful);
                rowsFailed = session.Query<FileImport>().Where(x => x.ImportId == message.ImportId).Count(x => !x.Successful);
            }

            var logMessage = string.Format("RowsSucceeded: {0}, RowsFailed: {1}, TotalNumberOfFilesInImport: {2}", rowsSucceeded, rowsFailed, message.TotalNumberOfFilesInImport);

            if (rowsSucceeded + rowsFailed == message.TotalNumberOfFilesInImport)
            {
                Log.Warn(logMessage + " import completed");
                bus.Publish(new FileImportCompleted { ImportId = message.ImportId });
            }
            else
            {
                Log.Warn(logMessage + " import not completed. Checking again for complete in 5 seconds.\n\r");
                bus.Defer(new TimeSpan(0, 0, 5), message);
            }
        }
    }
}
