using System;
using MediatR;
using Realtea.Infrastructure.Responses.Authentication;

namespace Realtea.Infrastructure.Commands.Authorization
{
	public class LoginUserCommand : IRequest<LoginUserResponse>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}

