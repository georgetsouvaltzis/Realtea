using System;
using Realtea.App.Requests.Account;
using Swashbuckle.AspNetCore.Filters;

namespace Realtea.Api.Examples
{
    public class EditAccountRequestExample : IExamplesProvider<EditAccountRequest>
    {
        public EditAccountRequest GetExamples()
        {
            return new EditAccountRequest
            {
                FirstName = "Developer"
            };
        }
    }
}

