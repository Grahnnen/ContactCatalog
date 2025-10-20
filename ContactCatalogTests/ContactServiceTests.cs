using Xunit;
using Moq;
using System.Collections.Generic;
using System.Linq;
using ContactCatalog;
using ContactCatalog.Repositories;
using ContactCatalog.Services;
using ContactCatalog.Models;

public class ContactServiceTests
{
    [Fact]
    public void Filter_By_Tag_Returns_Only_Matching()
    {
        var mock = new Mock<IContactRepository>();
        mock.Setup(r => r.GetAll()).Returns(new[]
        {
            new Contact { Id = 1, Name = "Anna", Email = "a@x.se", Tags = new List<string> { "friend" } },
            new Contact { Id = 2, Name = "Bo",   Email = "b@x.se", Tags = new List<string> { "work" } }
        });

        var svc = new ContactService(mock.Object);
        var result = svc.FilterByTag("friend").ToList();

        Assert.Single(result);
        Assert.Equal("Anna", result[0].Name);
    }
}