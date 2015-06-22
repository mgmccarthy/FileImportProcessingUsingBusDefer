using System;
using System.Linq;
using FileImportProcessingUsingBusDefer.Data;
using FileImportProcessingUsingBusDefer.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace FileImportProcessingUsingBusDefer.FileImportInsertionEndpoint.Handlers
{
    //public class FileImportInitiatedHandler : IHandleMessages<FileImportInitiated>
    //{
    //    private readonly IBus bus;
    //    private readonly IDataStore dataStore;
    //    private static readonly ILog Log = LogManager.GetLogger(typeof(FileImportInitiatedHandler));

    //    public FileImportInitiatedHandler(IBus bus, IDataStore dataStore)
    //    {
    //        this.bus = bus;
    //        this.dataStore = dataStore;
    //    }

    //    public void Handle(FileImportInitiated message)
    //    {
    //        int rowsSucceeded;
    //        int rowsFailed;
    //        using (var session = dataStore.OpenSession())
    //        {
    //            rowsSucceeded = session.Query<FileImport>().Where(x => x.ImportId == message.ImportId).Count(x => x.Successfull);
    //            rowsFailed = session.Query<FileImport>().Where(x => x.ImportId == message.ImportId).Count(x => !x.Successfull);
    //        }

    //        if (rowsSucceeded + rowsFailed >= message.TotalNumberOfFilesInImport)
    //        {
    //            Log.WarnFormat("import completed");
    //            bus.Publish(new FileImportCompleted { ImportId = message.ImportId });
    //        }
    //        else
    //        {
    //            Log.WarnFormat("import not completed. Checking again for complete in 5 seconds");
    //            bus.Defer(new TimeSpan(0, 0, 5), message);
    //        }
    //    }
    //}
}
