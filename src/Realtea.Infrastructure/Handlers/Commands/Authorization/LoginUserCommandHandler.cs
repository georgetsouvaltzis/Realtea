using System;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Realtea.Core.Entities;
using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Infrastructure.Authentication;
using Realtea.Infrastructure.Commands.Authorization;
using Realtea.Infrastructure.Identity;
using Realtea.Infrastructure.Results.Auth;

namespace Realtea.Infrastructure.Handlers.Commands.Authorization
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand, LoginUserResult>
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

        public async Task<LoginUserResult> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            if (request == null)
                throw new ArgumentNullException(nameof(request));

            var existingUser = await _userManager.FindByNameAsync(request.UserName);

            if (existingUser == null)
                throw new ApiException($"{request.UserName} does not exist.", FailureType.Absent);

            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, request.Password, false);

            if (!result.Succeeded)
                throw new ApiException($"Failed to log in.", FailureType.InvalidData);

            var generatedToken = await _jwtProvider.GenerateAsync(existingUser);

            return new LoginUserResult
            {
                Token = generatedToken
            };
        }
    }
}

