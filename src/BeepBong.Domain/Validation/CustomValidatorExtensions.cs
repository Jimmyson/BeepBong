using FluentValidation;
using FluentValidation.Validators;

namespace BeepBong.Domain.Validation
{
	public static class CustomValidatorExtensions
	{
		public static IRuleBuilderOptions<T, string> NoURLInString<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.SetValidator(new NoURLValidator());
		}
	}
}