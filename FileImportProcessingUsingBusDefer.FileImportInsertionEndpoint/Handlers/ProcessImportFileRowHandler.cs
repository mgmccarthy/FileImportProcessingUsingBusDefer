using System;
using FileImportProcessingUsingBusDefer.Data;
using FileImportProcessingUsingBusDefer.Messages.Commands;
using FileImportProcessingUsingBusDefer.Messages.Events;
using NServiceBus;
using NServiceBus.Logging;

namespace FileImportProcessingUsingBusDefer.FileImportInsertionEndpoint.Handlers
{
    public class ProcessImportFileRowHandler : IHandleMessages<ProcessImportFileRow>
    {
        private readonly IDataStore dataStore;
        private readonly IBus bus;

        public ProcessImportFileRowHandler(IDataStore dataStore, IBus bus)
        {
            this.dataStore = dataStore;
            this.bus = bus;
        }

        public void Handle(ProcessImportFileRow message)
        {
            if (message.FirstImportRowForThisImport)
                bus.Publish(new FileImportInitiated { ImportId = message.ImportId, TotalNumberOfFilesInImport = message.TotalNumberOfFilesInImport });

            var success = new Random().Next(100) % 2 == 0;
            LogManager.GetLogger(typeof(ProcessImportFileRowHandler)).WarnFormat(string.Format("Handling ProcessImportFileRow for Customer: {0}", message.CustomerId));

            using (var session = dataStore.OpenSession())
            {
                session.Add(new FileImport { Id = Guid.NewGuid(), ImportId = message.ImportId, CustomerId = message.CustomerId, CustomerName = message.CustomerName, Successfull = success });
                session.SaveChanges();
            }
        }
    }
}
