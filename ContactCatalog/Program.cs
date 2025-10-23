using Microsoft.Extensions.Logging;
using ContactCatalog.Repositories;
using ContactCatalog.Services;
using ContactCatalog.Validators;

namespace ContactCatalog
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //setup logger
            var loggerFactory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger<ContactService> logger = loggerFactory.CreateLogger<ContactService>();

            //setup UiService, respository and validator
            IContactRepository repository = new ContactRepository();
			ContactValidator validator = new ContactValidator();
			ContactService service = new ContactService(repository, logger, validator);
			MainUiService ui = new MainUiService(service, logger);

            ui.MainMenu();
        }
    }
}