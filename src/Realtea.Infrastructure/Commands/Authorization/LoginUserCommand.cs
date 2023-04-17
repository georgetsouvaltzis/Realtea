using System;
using MediatR;
using Realtea.Infrastructure.Results.Auth;

namespace Realtea.Infrastructure.Commands.Authorization
{
	public class LoginUserCommand : IRequest<LoginUserResult>
	{
		public string UserName { get; set; }
		public string Password { get; set; }
	}
}

