using MediatR;

namespace Realtea.Core.Commands.Advertisement
{
	public class DeleteAdvertisementCommand : IRequest
	{
		public int Id { get; set; }
	}
}

