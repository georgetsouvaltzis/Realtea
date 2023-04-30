namespace Realtea.App.Requests.Advertisement
{
	/// <summary>
	/// Incoming request for advertisement invalidation.
	/// </summary>
	public class DeleteAdvertisementRequest
	{
		/// <summary>
		/// Advertisement id.
		/// </summary>
		public int Id { get; set; }
	}
}

