using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Realtea.Core.DTOs.Authentication;
using Realtea.Core.Services;
using Realtea.Domain.Entities;

namespace Realtea.App.Controllers.V1
{    
    public class AuthController : V1ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole<int>> _roleManager;

        private readonly IJwtProvider _jwtProvider;

        public AuthController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole<int>> roleManager, IJwtProvider jwtProvider)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtProvider = jwtProvider;
            _roleManager = roleManager;
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


            // TODO: Try to send email?

            if (!result.Succeeded)
                throw new InvalidOperationException($"failed to create user. Failure reason: {string.Join(",", result.Errors.Select(x => x.Description))}");

            return Ok();
        }
       
        [HttpPost]
        [Route("generatetoken")]
        public async Task<ActionResult> GenerateToken([FromBody] LoginUserDto loginUserDto)
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

            return Ok(generatedToken);
        }
    }
}
