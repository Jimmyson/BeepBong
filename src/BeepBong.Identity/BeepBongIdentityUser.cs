namespace BeepBong.Identity
{
    public class BeepBongIdentityUser
    {
		public string Id { get; set; }
		public string UserName { get; set; }
		public string NormalisedUserName { get; set; }
		public string Email { get; set; }
		public string NormalisedEmail { get; set; }
		public bool EmailConfirmed { get; set; }
		public string PasswordHash { get; set; }
    }
}