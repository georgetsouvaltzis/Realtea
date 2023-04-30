namespace Realtea.App.Requests.Advertisement
{
	/// <summary>
	/// Incoming request for advertisement retrieval with given ID.
	/// </summary>
	public class ReadAdvertisementRequest
	{
		/// <summary>
		/// Advertisement ID.
		/// </summary>
		public int Id { get; set; }
	}
}

