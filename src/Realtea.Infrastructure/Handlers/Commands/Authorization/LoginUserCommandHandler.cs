using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Realtea.Core.Entities;
using Realtea.Infrastructure.Authentication;
using Realtea.Infrastructure.Commands.Authorization;
using Realtea.Infrastructure.Identity;
using Realtea.Infrastructure.Responses.Authentication;

namespace Realtea.Infrastructure.Handlers.Commands.Authorization
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResponse>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtProvider _jwtProvider;

        public LoginUserCommandHandler(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
        }

        public async Task<LoginUserResponse> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser == null)
                throw new InvalidOperationException($"{request.UserName} does not exist.");

            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, request.Password, false);

            if (!result.Succeeded)
                throw new InvalidOperationException($"Failed to log in.");

            var generatedToken = _jwtProvider.Generate(existingUser);

            return new LoginUserResponse
            {
                Token = generatedToken
            };
        }
    }
}

