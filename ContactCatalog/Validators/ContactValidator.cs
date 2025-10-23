using Castle.Core.Resource;
using ContactCatalog.Models;
using ContactCatalog.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactCatalog.Validators
{
	public class ContactValidator
	{
		public void Validate(Contact contact)
		{
			if (string.IsNullOrWhiteSpace(contact.Name))//if name is empty
				throw new InvalidContactException($"Invalid name{contact.Name}");
			if (!IsValidEmail(contact.Email))//if invalid email
			{
				throw new InvalidContactException($"Invalid email {contact.Email}");
			}
		}
		public bool IsValidEmail(string email)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(email))//if email is empty
					return false;
				var addr = new System.Net.Mail.MailAddress(email); //format the sent email parameter according to built in mailadressmethod
				return addr.Address == email;//true if the email stays the same
			}
			catch 
			{
				return false;
			}
		}
	}
}