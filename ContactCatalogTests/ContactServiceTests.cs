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
	[Fact]
	public void LogsWarning_WhenValidationFails()
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
}