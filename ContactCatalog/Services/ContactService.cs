using ContactCatalog.Models;
using ContactCatalog.Repositories;
using System.Collections.Generic;
using System.Linq;

namespace ContactCatalog.Services
{
    public class ContactService
    {
        private readonly IContactRepository _repo;

        public ContactService(IContactRepository repo)
        {
            _repo = repo;
        }

        public IEnumerable<Contact> GetAll()
        {
            return _repo.GetAll();//Returns all contacts
        }

        public IEnumerable<Contact> SearchByName(string term)
        {
            return _repo.GetAll()
                .Where(c => c.Name.Contains(term, StringComparison.OrdinalIgnoreCase))
                .OrderBy(c => c.Name); //returns all usernames and ignores small or big letters
        }

        public IEnumerable<Contact> FilterByTag(string tag)
        {
            return _repo.GetAll()
                .Where(c => c.Tags.Contains(tag, StringComparer.OrdinalIgnoreCase))
                .OrderBy(c => c.Name);//Return all contacts which contains tag and ignores small or big letters
        }

        public void Add(Contact contact)
        {
            _repo.Add(contact);
        }
    }
}