using System;

namespace FileImportProcessingUsingBusDefer.Messages.Events
{
    public class FileImportInitiated
    {
        public Guid ImportId { get; set; }
        public int TotalNumberOfFilesInImport { get; set; }
    }
}
