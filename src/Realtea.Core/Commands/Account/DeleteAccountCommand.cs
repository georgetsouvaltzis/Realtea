using System;
using MediatR;

namespace Realtea.Core.Commands.Account
{
	public class DeleteAccountCommand : IRequest
	{
		public int UserId { get; set; }
	}
}

