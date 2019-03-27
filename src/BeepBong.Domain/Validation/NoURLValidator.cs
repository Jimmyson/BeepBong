using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
	public static class NoURLValidator
	{
		public static IRuleBuilderOptions<T, string> NoURLInString<T>(this IRuleBuilder<T, string> ruleBuilder)
		{
			return ruleBuilder.Must(s => !s.Contains("http:")).WithMessage("The value contains a URL");
		}
	}
}