using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BeepBong.Identity
{
	public class BeepBongIdentityUserStore :
		IUserStore<BeepBongIdentityUser>,
		IUserEmailStore<BeepBongIdentityUser>,
		IUserPasswordStore<BeepBongIdentityUser>
	{
		public BeepBongIdentityUserStore(BeepBongIdentityContext context)
		{
			_context = context;
		}

		public BeepBongIdentityContext _context { get; set; }

		public async Task<IdentityResult> CreateAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
				throw new ArgumentNullException(nameof(user));

			_context.Users.Add(user);
			await _context.SaveChangesAsync(cancellationToken);

			return IdentityResult.Success;
		}

		public async Task<IdentityResult> DeleteAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));
				
            _context.Remove(user);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                return IdentityResult.Failed();
            }
            return IdentityResult.Success;
		}

		public void Dispose()
		{
		}

		public async Task<BeepBongIdentityUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken)
		{
			return await _context.Users.SingleOrDefaultAsync(u => u.NormalisedEmail == normalizedEmail);
		}

		public async Task<BeepBongIdentityUser> FindByIdAsync(string userId, CancellationToken cancellationToken)
		{
			return await _context.Users.SingleOrDefaultAsync(u => u.Id == userId);
		}

		public async Task<BeepBongIdentityUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken)
		{
			return await _context.Users.SingleOrDefaultAsync(u => u.NormalisedUserName == normalizedUserName);
		}

		public Task<string> GetEmailAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.Email);
		}

		public Task<bool> GetEmailConfirmedAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.EmailConfirmed);
		}

		public Task<string> GetNormalizedEmailAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.NormalisedEmail);
		}

		public Task<string> GetNormalizedUserNameAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.NormalisedUserName);
		}

		public Task<string> GetPasswordHashAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.PasswordHash);
		}

		public Task<string> GetUserIdAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.Id);
		}

		public Task<string> GetUserNameAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.UserName);
		}

		public Task<bool> HasPasswordAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			return Task.FromResult(user.PasswordHash != null);
		}

		public Task SetEmailAsync(BeepBongIdentityUser user, string email, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			user.Email = email;
			return Task.CompletedTask;
		}

		public Task SetEmailConfirmedAsync(BeepBongIdentityUser user, bool confirmed, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			user.EmailConfirmed = confirmed;
			return Task.CompletedTask;
		}

		public Task SetNormalizedEmailAsync(BeepBongIdentityUser user, string normalizedEmail, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			user.NormalisedEmail = normalizedEmail;
			return Task.CompletedTask;
		}

		public Task SetNormalizedUserNameAsync(BeepBongIdentityUser user, string normalizedName, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			user.NormalisedUserName = normalizedName;
			return Task.CompletedTask;
		}

		public Task SetPasswordHashAsync(BeepBongIdentityUser user, string passwordHash, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			user.PasswordHash = passwordHash;
			return Task.CompletedTask;
		}

		public Task SetUserNameAsync(BeepBongIdentityUser user, string userName, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

			user.UserName = userName;
			return Task.CompletedTask;
		}

		public async Task<IdentityResult> UpdateAsync(BeepBongIdentityUser user, CancellationToken cancellationToken)
		{
			if (user == null)
                throw new ArgumentNullException(nameof(user));

            _context.Attach(user);
            //user.ConcurrencyStamp = Guid.NewGuid().ToString();
            //_context.Update(user);
            try
            {
                await _context.SaveChangesAsync(cancellationToken);
            }
            catch (DbUpdateConcurrencyException)
            {
                //return IdentityResult.Failed(ErrorDescriber.ConcurrencyFailure());
                return IdentityResult.Failed();
            }
            return IdentityResult.Success;
		}
	}
}