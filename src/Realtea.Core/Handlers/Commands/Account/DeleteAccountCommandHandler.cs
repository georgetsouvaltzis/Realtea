using System;
using MediatR;
using Realtea.Core.Commands.Account;
using Realtea.Core.Interfaces.Repositories;

namespace Realtea.Core.Handlers.Commands.Account
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteAccountCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(request.UserId);
        }
    }
}

