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
		private readonly Dictionary<int, Contact> _contacts = new();
		private readonly HashSet<string> _emails = new(StringComparer.OrdinalIgnoreCase); //ignores big or small letters

		public IEnumerable<Contact> GetAll() => _contacts.Values;//Returns all contacts in _contacts

		public void Add(Contact contact)
		{
			ContactValidator.Validate(contact);//Validate User input
			if (!_emails.Add(contact.Email))//if duplicate email
				throw new DuplicateEmailException(contact.Email);

			//Success! add the customer to dictionary
			_contacts.Add(contact.Id, contact);
		}
	}
}