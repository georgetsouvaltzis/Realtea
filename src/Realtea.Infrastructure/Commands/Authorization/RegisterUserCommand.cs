using System;
using MediatR;

namespace Realtea.Infrastructure.Commands.Authorization
{
    public class RegisterUserCommand : IRequest
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string ConfirmedPassword { get; set; }
    }
}

