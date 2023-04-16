using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Realtea.Core.Entities;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Infrastructure.Commands.Authorization;
using Realtea.Infrastructure.Identity;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Realtea.Infrastructure.Handlers.Commands.Authorization
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand>
    {
        //private readonly UserManager<ApplicationUser> _userManager;
        private readonly IUserRepository _userRepository;

        public RegisterUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            if (request.Password != request.ConfirmedPassword)
                throw new InvalidOperationException("Passwords do not match.");

            var existingUser = await _userRepository.GetByUsernameAsync(request.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"{request.UserName} is taken.");

            var newUser = new User
            {
                UserName = request.UserName,
            };

            var result = await _userRepository.CreateAsync(newUser, request.Password);

            if (result == 0)
                //throw new InvalidOperationException($"failed to create user. Failure reason: {string.Join(",", result.Errors.Select(x => x.Description))}");
                throw new InvalidOperationException("Failed to create user");
        }
    }
}

