using System;
using MediatR;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Queries
{
    public class ReadAdvertisementQuery : IRequest<AdvertisementResult>
    {
        public int Id { get; set; }
    }
}

