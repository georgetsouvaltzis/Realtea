using MediatR;
using Realtea.Core.Commands.Account;

namespace Realtea.Core.Handlers.Commands.Account
{
    public class UpgradeAccountCommandHandler : IRequestHandler<UpgradeAccountCommand>
    {

        public async Task Handle(UpgradeAccountCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}

