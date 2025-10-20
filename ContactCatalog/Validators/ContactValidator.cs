using Castle.Core.Resource;
using ContactCatalog.Models;
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
				throw new InvalidNameException(contact.Name);
			if (!IsValidEmail(contact.Email))//if invalid email
			{
				throw new InvalidEmailException(contact.Email);
			}
		}
		public bool IsValidEmail(string email)
		{
			try
			{
				if (string.IsNullOrWhiteSpace(email))//if email is empty
					return false;
				var addr = new System.Net.Mail.MailAddress(email);
				return addr.Address == email;
			}
			catch 
			{
				return false;
			}
		}
	}

}
