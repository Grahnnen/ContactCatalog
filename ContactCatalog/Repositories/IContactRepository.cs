using ContactCatalog.Models;

namespace ContactCatalog.Repositories
{
    public interface IContactRepository
    {
        IEnumerable<Contact> GetAll();
        void Add(Contact contact);
    }
}
//Asked AI why an interface would be needed here and this was the response:

//An interface makes your code more flexible, testable, and future-proof.
//It’s a fundamental principle in modern software development,
//especially when you want to write unit tests and use dependency injection.