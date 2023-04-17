using System;
using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Commands.Advertisement
{
    public class UpdateAdvertisementCommand : IRequest<UpdateAdvertisementResult>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        // DO NOT FORGET TO CHANGE IT.
        public AdvertisementType? AdvertisementType { get; set; }

        public DealType? DealType { get; set; }

        public decimal? Price { get; set; }

        public decimal? SquareMeter { get; set; }

        public Location? Location { get; set; }

        public bool? IsActive { get; set; }
    }
}

