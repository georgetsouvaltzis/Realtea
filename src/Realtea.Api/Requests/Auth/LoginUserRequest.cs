namespace Realtea.App.Requests.Auth
{
	/// <summary>
	/// Login user request credentials.
	/// </summary>
	public class LoginUserRequest
	{
		/// <summary>
		/// Registered username.
		/// </summary>
		public string UserName { get; set; }

		/// <summary>
		/// Password for the given username.
		/// </summary>
		public string Password { get; set; }
	}
}

