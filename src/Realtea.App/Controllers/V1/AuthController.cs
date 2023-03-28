using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Realtea.Core.DTOs.Authentication;
using Realtea.Core.Services;
using Realtea.Domain.Entities;

namespace Realtea.App.Controllers.V1
{
    [Route("/api/v1/[controller]")]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtProvider _jwtProvider;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
        }

        [HttpPost]
        [Route("register")]
        public async Task<ActionResult> Register([FromBody] RegisterUserDto registerUserDto)
        {
            if (registerUserDto == null)
                throw new ArgumentNullException(nameof(registerUserDto));

            if (registerUserDto.Password != registerUserDto.ConfirmedPassword)
                throw new InvalidOperationException("Passwords do not match.");

            var existingUser = await _userManager.FindByNameAsync(registerUserDto.UserName);

            if (existingUser != null)
                throw new InvalidOperationException($"{registerUserDto.UserName} is taken.");


            var newUser = new User
            {
                UserName = registerUserDto.UserName,
            };

            var result = await _userManager.CreateAsync(newUser, registerUserDto.Password);

            if (!result.Succeeded)
                throw new InvalidOperationException($"failed to create user. Failure reason: {string.Join(",", result.Errors.Select(x => x.Description))}");

            //await _signInManager.SignInAsync(newUser, false);

            return Ok();
        }

        [HttpPost]
        [Route("login")]
        public async Task<ActionResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            if (loginUserDto == null)
                throw new ArgumentNullException(nameof(loginUserDto));

            var existingUser = await _userManager.FindByNameAsync(loginUserDto.UserName);

            if (existingUser == null)
                throw new InvalidOperationException($"{loginUserDto.UserName} does not exist.");

            var result = await _signInManager.CheckPasswordSignInAsync(existingUser, loginUserDto.Password, false);

            if (!result.Succeeded)
                throw new InvalidOperationException($"Failed to log in.");

            var generatedToken = _jwtProvider.Generate(existingUser);

            //await _signInManager.SignInAsync(existingUser, false);
            return Ok(generatedToken);
            
        }
    }
}
