using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Realtea.Core.Entities;
using Realtea.Infrastructure.Commands.Authorization;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Realtea.Infrastructure.Handlers.Commands.Authorization
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        private readonly UserManager<User> _userManager;

        public RegisterUserCommandHandler(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Password != request.ConfirmedPassword)
                throw new InvalidOperationException("Passwords do not match.");

            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"{request.UserName} is taken.");

            var newUser = new User
            {
                UserName = request.UserName,
            };

            var result = await _userManager.CreateAsync(newUser, request.Password);

            if (!result.Succeeded)
                throw new InvalidOperationException($"failed to create user. Failure reason: {string.Join(",", result.Errors.Select(x => x.Description))}");
        }
    }
}

