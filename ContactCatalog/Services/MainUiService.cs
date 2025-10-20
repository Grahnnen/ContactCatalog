using ContactCatalog.Models;
using ContactCatalog.Validators;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Services
{
	public class MainUiService
	{
		private readonly ContactService _contactService;
		private readonly ILogger _logger;
		public MainUiService(ContactService service, ILogger<ContactService> logger)
		{
			_contactService = service;
			this._logger = logger;
		}

		public void MainMenu()
		{
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
						AddContact();
						break;
					case "2":
						ListContacts();
						break;
					case "3":
						SearchContacts();
						break;
					case "4":
						FilterByTag();
						break;
					case "5":
						ExportCsv();
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

		public void AddContact()
		{
			Console.Write("Name: ");
			string name = Console.ReadLine();
			Console.Write("Email: ");
			string email = Console.ReadLine();
			Console.Write("Tags (komma-separerade): ");
			var tags = Console.ReadLine() 
				.Split(',', StringSplitOptions.RemoveEmptyEntries)
				.Select(t => t.Trim())
				.ToList(); //Saves all tags separated by comma

			var contact = new Contact {Name = name, Email = email, Tags = tags };//Create new contact object

			_contactService.Add(contact);
		}

		public void ListContacts()
		{
			var contacts = _contactService.GetAll().OrderBy(c => c.Name); //Gets all usernames
			foreach (var c in contacts)
				Console.WriteLine($"- ({c.Id}) {c.Name} <{c.Email}> [{string.Join(",", c.Tags)}]"); //Writes all usernames
			Console.WriteLine();
		}

		public void SearchContacts()
		{
			Console.Write("Sökterm för namn: ");
			var term = Console.ReadLine();
			var hits = _contactService.SearchByName(term); //Gets all users by searchtag
			foreach (var c in hits)
				Console.WriteLine($"- ({c.Id}) {c.Name} <{c.Email}> [{string.Join(",", c.Tags)}]");//Writes all users with searchTerm
			Console.WriteLine();
		}

		public void FilterByTag()
		{
			Console.Write("Tag: ");
			var tag = Console.ReadLine();
			var hits = _contactService.FilterByTag(tag); //Gets all users with tag
			foreach (var c in hits)
				Console.WriteLine($"- ({c.Id}) {c.Name} <{c.Email}> [{string.Join(",", c.Tags)}]"); //Writes out all users with tag
			Console.WriteLine();
		}

		public void ExportCsv()
		{
			Console.Write("Exportfil(Enter defaults to contacts.csv): ");
			var fileInput = Console.ReadLine();
			var file = string.IsNullOrWhiteSpace(fileInput) ? "contacts.csv" : fileInput; //if empty default to contacts.csv else use userinput
			try
			{
				var csv = ToCsv(_contactService.GetAll()); //Get all contacts and formats it
				File.WriteAllText(file, csv); //Writes the information to file
				_logger.LogInformation("Exported {Count} contacts to {File}", _contactService.GetAll().Count(), Path.GetFullPath(file));
				Console.WriteLine($"{_contactService.GetAll().Count()} kontakter/exporterad.\n");
			}
			catch (Exception ex)
			{
				_logger.LogError(ex, "Error exporting CSV");
				Console.WriteLine("Kunde inte exportera.");
			}
		}

		private string ToCsv(IEnumerable<Contact> contacts)
		{
			var sb = new StringBuilder();
			sb.AppendLine("Id|Name|Email|Tags"); //Formats the text to file
			foreach (var c in contacts)
			{
				var tags = string.Join(',', c.Tags); //formats the tags
				sb.AppendLine($"{c.Id}|{c.Name}|{c.Email}|{tags}"); //Adds the variables
			}
			return sb.ToString();//return formatted string
		}
	}
}
