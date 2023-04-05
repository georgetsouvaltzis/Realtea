using System;
using MediatR;
using Realtea.Core.Commands.Advertisement;
using Realtea.Core.Interfaces.Repositories;

namespace Realtea.Core.Handlers.Commands.Advertisement
{
    public class DeleteAdvertisementCommandHandler : IRequestHandler<DeleteAdvertisementCommand>
    {
        private readonly IAdvertisementRepository _advertisementRepository;

        public DeleteAdvertisementCommandHandler(IAdvertisementRepository advertisementRepository)
        {
            _advertisementRepository = advertisementRepository;
        }
        public async Task Handle(DeleteAdvertisementCommand request, CancellationToken cancellationToken)
        {
            await _advertisementRepository.InvalidateAsync(request.Id);
        }
    }
}

