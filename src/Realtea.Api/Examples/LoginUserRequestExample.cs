using System;
using Realtea.App.Requests.Auth;
using Swashbuckle.AspNetCore.Filters;

namespace Realtea.Api.Examples
{
    public class LoginUserRequestExample : IExamplesProvider<LoginUserRequest>
    {
        public LoginUserRequest GetExamples()
        {
            return new LoginUserRequest
            {
                UserName = "testuser1",
                Password = "Test1"
            };
        }
    }
}

