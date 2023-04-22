using System;
using MediatR;

namespace Realtea.Infrastructure.Commands.User
{
	public class AddUserToBrokerRoleCommand : IRequest
	{
		public int UserId { get; set; }
	}
}

