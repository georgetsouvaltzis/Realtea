using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Exceptions;
using Realtea.Core.Interfaces.Repositories;
using Realtea.Infrastructure.Commands.User;

namespace Realtea.Infrastructure.Handlers.Commands.User
{
    public class AddUserToBrokerRoleCommandHandler : IRequestHandler<AddUserToBrokerRoleCommand>
    {
        private readonly IUserRepository _userRepository;
        public AddUserToBrokerRoleCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task Handle(AddUserToBrokerRoleCommand request, CancellationToken cancellationToken)
        {
            var isInRole = await _userRepository.IsInBrokerRoleAsync(request.UserId);

            if (isInRole)
                throw new ApiException("User is already in Broker role.", FailureType.Conflict);

            await _userRepository.UpgradeToBrokerAsync(request.UserId);
        }
    }
}


