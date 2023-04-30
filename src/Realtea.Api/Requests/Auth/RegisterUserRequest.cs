namespace Realtea.App.Requests.Auth
{
	/// <summary>
	/// User registration request data.
	/// </summary>
	public class RegisterUserRequest
	{
		/// <summary>
		/// Username for the user.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Password for logging-in.
		/// </summary>
		public string Password { get; set; }

		/// <summary>
		/// Double-confirmation of the Password.
		/// </summary>
		public string ConfirmedPassword { get; set; }
	}
}

