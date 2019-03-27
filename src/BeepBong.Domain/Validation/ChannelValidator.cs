using BeepBong.Domain.Models;
using FluentValidation;

namespace BeepBong.Domain.Validation
{
	public class ChannelValidator : AbstractValidator<Channel>
	{
		public ChannelValidator()
		{
			RuleFor(c => c.Name).NotNull().NotEmpty().NoURLInString();
			RuleFor(c => c.Organisation).NoURLInString();
		}
	}
}