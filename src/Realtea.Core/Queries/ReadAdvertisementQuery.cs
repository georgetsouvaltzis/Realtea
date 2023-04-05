using System;
using MediatR;
using Realtea.Core.Responses;
using Realtea.Core.Responses.Advertisement;

namespace Realtea.Core.Queries
{
    public class ReadAdvertisementQuery : IRequest<ReadAdvertisementsResponse>
    {
        public int Id { get; set; }
    }
}

