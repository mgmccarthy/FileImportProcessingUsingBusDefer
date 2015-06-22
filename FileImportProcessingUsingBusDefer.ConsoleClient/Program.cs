﻿using System;
using System.Threading;
using FileImportProcessingUsingBusDefer.Messages.Commands;

namespace FileImportProcessingUsingBusDefer.ConsoleClient
{
    class Program
    {
        static void Main(string[] args)
        {
            ServiceBus.Init();

            Console.WriteLine("Press 'Enter' to initiate a new file import.To exit, Ctrl + C");

            while (Console.ReadLine() != null)
            {
                var importId = Guid.NewGuid();
                const int totalNumberOfFilesInImport = 1000;

                for (var i = 1; i <= totalNumberOfFilesInImport; i++)
                {
                    Thread.Sleep(50);
                    Console.WriteLine("Sending ProcessImportFileRow for Customer: {0}", i);

                    var processFileRow = new ProcessImportFileRow
                    {
                        ImportId = importId,
                        CustomerId = i,
                        CustomerName = string.Format("Customer: {0}", i),
                        FirstImportRowForThisImport = i == 1,
                        TotalNumberOfFilesInImport = totalNumberOfFilesInImport
                    };
                    ServiceBus.Bus.Send(processFileRow);
                }
            }
        }
    }
}
