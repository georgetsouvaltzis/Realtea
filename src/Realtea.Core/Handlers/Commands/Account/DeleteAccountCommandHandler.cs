using System;
using MediatR;
using Realtea.Core.Commands.Account;
using Realtea.Core.Interfaces.Repositories;

namespace Realtea.Core.Handlers.Commands.Account
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAdvertisementRepository _advertisementRepository;

        public DeleteAccountCommandHandler(IUserRepository userRepository, IAdvertisementRepository advertisementRepository)
        {
            _userRepository = userRepository;
            _advertisementRepository = advertisementRepository;
        }

        public async Task Handle(DeleteAccountCommand request, CancellationToken cancellationToken)
        {
            await _userRepository.DeleteAsync(request.UserId);

            var associatedAds = _advertisementRepository.GetAsQueryable().Where(x => x.UserId == request.UserId).ToList();

            var taskLists = new List<Task>(associatedAds.Count);

            associatedAds.ForEach(x => taskLists.Add(_advertisementRepository.InvalidateAsync(x.Id)));

            Task.WaitAll(taskLists.ToArray());
        }
    }
}

