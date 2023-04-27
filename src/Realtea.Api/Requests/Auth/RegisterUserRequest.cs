namespace Realtea.App.Requests.Auth
{
	public class RegisterUserRequest
	{
		public string UserName { get; set; }

		public string Password { get; set; }

		public string ConfirmedPassword { get; set; }
	}
}

