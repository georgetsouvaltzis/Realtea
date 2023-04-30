namespace Realtea.App.Requests.Account
{
    /// <summary>
    /// Request for editing Account.
    /// </summary>
    public class EditAccountRequest
	{
        /// <summary>
        /// Name of the given user.
        /// </summary>
        public string? FirstName { get; set; }

        /// <summary>
        /// Last name of the given user.
        /// </summary>
        public string? LastName { get; set; }

        /// <summary>
        /// Email of the given user.
        /// </summary>
        public string? Email { get; set; }
    }
}

