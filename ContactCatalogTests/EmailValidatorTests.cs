using ContactCatalog.Validators;

namespace ContactCatalogTests
{
	public class EmailValidatorTests
	{
		[Theory]
		[InlineData("test@example.com", true)]
		[InlineData("invalid-email", false)]
		public void Given_ValidEmail_When_Validated_Then_ShouldReturnTrue(string email, bool expected)
		{
			Assert.Equal(expected, ContactValidator.IsValidEmail(email));
		}
	}
}