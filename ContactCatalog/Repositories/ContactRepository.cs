using ContactCatalog.Models;
using ContactCatalog.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Repositories
{
	public class ContactRepository : IContactRepository
	{
		//dictionary because need to find contacts bades on id
		private readonly Dictionary<int, Contact> _contacts = new(); 
		//Hashset because they are unique
		private readonly HashSet<string> _emails = new(StringComparer.OrdinalIgnoreCase); //ignores big or small letters

		public IEnumerable<Contact> GetAll() => _contacts.Values;//Returns all contacts in _contacts

		public void Add(Contact contact)
		{
			if (!_emails.Add(contact.Email))//if duplicate email throw error but if its unique add it to the Hashset
				throw new Exception($"Email already exist! {contact.Email}");
			//add the customer to dictionary
			_contacts.Add(contact.Id, contact);
		}
	}
}