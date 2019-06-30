using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace BeepBong.Web
{
    public class EmailSender : IEmailSender
	{
		//@TODO: Send a confirmation email
		public Task SendEmailAsync(string email, string subject, string message)
		{
			return Task.CompletedTask;
		}
	}
}