using System;
using MediatR;
using Realtea.Core.Commands.Account;
using Realtea.Core.Interfaces.Repositories;

namespace Realtea.Core.Handlers.Commands.Account
{
	public class EditAccountCommandHandler : IRequestHandler<EditAccountCommand>
	{
		private readonly IUserRepository _userRepository;
		public EditAccountCommandHandler(IUserRepository userRepository)
		{
			_userRepository = userRepository;
		}

        public async Task Handle(EditAccountCommand request, CancellationToken cancellationToken)
        {
			var existingUser = await _userRepository.GetByIdAsync(request.UserId.ToString());

            if (request.FirstName != null)
                existingUser.FirstName = request.FirstName;

            if (request.LastName != null)
                existingUser.LastName = request.LastName;

            if (request.Email != null)
                existingUser.Email = request.Email;

            await _userRepository.UpdateAsync(existingUser);

        }
    }
}

