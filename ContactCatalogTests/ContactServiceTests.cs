using Castle.Core.Resource;
using ContactCatalog;
using ContactCatalog.Models;
using ContactCatalog.Repositories;
using ContactCatalog.Services;
using ContactCatalog.Validators;
using Microsoft.Extensions.Logging;
using Moq;
using System.Collections.Generic;
using System.Linq;
using Xunit;

public class ContactServiceTests
{
	//This ensures that an exception is thrown once when validation fails
	[Fact]
	public void Given_Contact_When_Validation_Fails_Then_ShouldLogWarning()
	{
		var repoMock = new Mock<IContactRepository>();
		var loggerMock = new Mock<ILogger<ContactService>>();
		var validator = new ContactValidator();
		var service = new ContactService(repoMock.Object, loggerMock.Object, validator);

		var invalidCustomer = new Contact { Name = "", Email = "test@example.com" };
		service.Add(invalidCustomer);

		loggerMock.Verify(
			x => x.Log(
				LogLevel.Warning,
				It.IsAny<EventId>(),
				It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("Valideringsfel")),
				It.IsAny<Exception>(),
				It.IsAny<Func<It.IsAnyType, Exception, string>>()),
			Times.Once);
	}
	//This tests if the ID increases if an invalid contact is added
	[Fact]
	public void Given_Contact_When_Added_Then_IdsIncrementOnlyForValidContacts()
	{
		var repoMock = new Mock<IContactRepository>();
		var loggerMock = new Mock<ILogger<ContactService>>();
		var validator = new ContactValidator();
		var service = new ContactService(repoMock.Object, loggerMock.Object, validator);

		//valid contact
		var contact1 = new Contact { Name = "Alice", Email = "alice@example.com" };
		service.Add(contact1);
		Assert.Equal(1, contact1.Id);

		//invalid contact
		var contact2 = new Contact { Name = "", Email = "bob@example.com" };
		service.Add(contact2);
		Assert.Equal(0, contact2.Id); //Checks if the id still is 0 which it should be,
									  //because it couldnt add a contact and cant increment the id

		//another valid contact
		var contact3 = new Contact { Name = "Charlie", Email = "charlie@example.com" };
		service.Add(contact3);
		Assert.Equal(2, contact3.Id);
	}
}