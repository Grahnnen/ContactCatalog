using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using ContactCatalog.Repositories;
using ContactCatalog.Services;
using ContactCatalog.Validators;

namespace ContactCatalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //setup logger
            using var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger<ContactService> logger = loggerFactory.CreateLogger<ContactService>();

            //setup UiService and respository
            IContactRepository repository = new ContactRepository();
            var validator = new ContactValidator();
            var service = new ContactService(repository, logger, validator);
            var ui = new ContactUiService(service, logger, validator);

            while (true)
            {
                Console.Clear();
                Console.WriteLine("Contact Catalog");
                Console.WriteLine("1) Add");
                Console.WriteLine("2) List");
                Console.WriteLine("3) Search");
                Console.WriteLine("4) Filter by Tag");
                Console.WriteLine("5) Export CSV");
                Console.WriteLine("0) Exit");
                Console.Write("Choice: ");
                var choice = Console.ReadLine();

                Console.Clear();

                //Calls the function depending on your choice
                switch (choice)
                {
                    case "1":
                        ui.AddContact();
                        break;
                    case "2":
                        ui.ListContacts();
                        break;
                    case "3":
                        ui.SearchContacts();
                        break;
                    case "4":
                        ui.FilterByTag();
                        break;
                    case "5":
                        ui.ExportCsv();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice.");
                        break;
                }

                //Pauses program until you press a key
                Console.WriteLine("\nTryck på valfri tangent för att fortsätta...");
                Console.ReadKey();
            }
        }
    }
}
