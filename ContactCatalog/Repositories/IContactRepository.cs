using ContactCatalog.Models;

namespace ContactCatalog.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        void Add(Contact contact);
    }
}