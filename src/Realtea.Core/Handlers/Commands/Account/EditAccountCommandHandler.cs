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

            if(!string.IsNullOrEmpty(request.FirstName))
                existingUser.ChangeFirstName(request.FirstName);

            if (!string.IsNullOrEmpty(request.LastName))
                existingUser.ChangeLastName(request.LastName);

            if (!string.IsNullOrEmpty(request.Email))
                existingUser.ChangeEmail(request.Email);

            await _userRepository.UpdateAsync(existingUser);
        }
    }
}

