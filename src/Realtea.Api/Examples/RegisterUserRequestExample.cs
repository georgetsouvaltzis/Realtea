using System;
using Realtea.App.Requests.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace Realtea.Api.Examples
{
    public class RegisterUserRequestExample : IExamplesProvider<RegisterUserRequest>
    {
        public RegisterUserRequest GetExamples()
        {
            return new RegisterUserRequest
            {
                UserName = "testuser1",
                Password = "Test1",
                ConfirmedPassword = "Test1"
            };
        }
    }
}

