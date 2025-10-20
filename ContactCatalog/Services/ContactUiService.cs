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
	public class ContactUiService
	{
		private readonly ContactService _contactService;
		private readonly ILogger _logger;
		private readonly ContactValidator _validator;

		private int id = 1; //ContactID
		public ContactUiService(ContactService service, ILogger<ContactService> logger, ContactValidator validator)
		{
			_contactService = service;
			this._logger = logger;
			_validator = validator;
		}

		public void AddContact()
		{
			try
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

				var contact = new Contact { Id = id, Name = name, Email = email, Tags = tags };//Create new contact 
				_contactService.Add(contact);//Try add it
				id++;//increament contactID

				_logger.LogInformation("Contact added: {Email}", email);
				Console.WriteLine($"\nLade till {_contactService.GetAll().Count()} kontakt i katalogen.\n");
				
			}
			catch (DuplicateEmailException ex)//If duplicate email
			{
				_logger.LogWarning("Duplicate email: {Email}", ex.Message);
				Console.WriteLine("E-postadressen finns redan.");
			}
			catch (InvalidEmailException ex)//if invalid email
			{
				_logger.LogWarning("Invalid email: {Email}", ex.Message);
				Console.WriteLine("Ogiltig e-postadress.");
			}
			catch(InvalidNameException ex)//if invalid name
			{
				_logger.LogWarning("Invalid name: {Name}", ex.Message);
				Console.WriteLine("Ogiltig namn.");
			}
			catch (Exception ex) //if some other error
			{
				_logger.LogError(ex, "Error adding contact");
				Console.WriteLine("Kunde inte lägga till kontakt.");
			}
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
			Console.Write("Exportfil: ");
			var file = Console.ReadLine() ?? "contacts.csv"; //if users just press enter default to contacts.csv
			try
			{
				var csv = ToCsv(_contactService.GetAll()); //Get all contacts and formats it
				File.WriteAllText(file, csv); //Writes the information to file
				_logger.LogInformation("Exported {Count} contacts to {File}", _contactService.GetAll().Count(), file);
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
			sb.AppendLine("Id,Name,Email,Tags"); //Formats the text to file
			foreach (var c in contacts)
			{
				var tags = string.Join('|', c.Tags);
				sb.AppendLine($"{c.Id},{c.Name},{c.Email},{tags}"); //Adds the variables
			}
			return sb.ToString();//return formatted string
		}
	}
}
