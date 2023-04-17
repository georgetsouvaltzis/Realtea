using System;
using MediatR;

namespace Realtea.Core.Commands.Account
{
	public class EditAccountCommand : IRequest
	{
		public int UserId { get; set; }
		public string? FirstName { get; set; }
		public string? LastName { get; set; }
		public string? Email { get; set; }
	}
}

