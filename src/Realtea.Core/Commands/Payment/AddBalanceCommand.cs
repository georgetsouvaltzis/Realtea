using System;
using MediatR;

namespace Realtea.Core.Commands.Payment
{
	public class AddBalanceCommand : IRequest
	{
		public int UserId { get; set; }
	}
}

