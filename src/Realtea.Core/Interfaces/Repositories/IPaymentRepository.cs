using Realtea.Core.Entities;

namespace Realtea.Core.Interfaces.Repositories
{
	/// <summary>
	/// Repository for Payments.
	/// </summary>
	public interface IPaymentRepository
	{
		/// <summary>
		/// Creates new Payment with given data.
		/// </summary>
		/// <param name="payment">Contains data regarding Payments entity.</param>
		/// <returns>ID of the created Payment.</returns>
		Task<int> CreateAsync(Payment payment);
	}
}
