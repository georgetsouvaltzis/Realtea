using System;
using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Responses.Advertisement;

namespace Realtea.Core.Commands.Advertisement
{
    public class UpdateAdvertisementCommand : IRequest<UpdateAdvertisementResponse>
    {
        public int Id { get; set; }
        public string? Name { get; set; }

        public string? Description { get; set; }

        // DO NOT FORGET TO CHANGE IT.
        public AdvertisementType? AdvertisementType { get; set; }

        public UpdateAdvertisementDetailsCommand? UpdateAdvertisementDetails { get; set; }

        public bool? IsActive { get; set; }


    }
    public class UpdateAdvertisementDetailsCommand
    {
        public DealTypeEnum? DealType { get; set; }

        public decimal? Price { get; set; }

        public decimal? SquareMeter { get; set; }

        public LocationEnum? Location { get; set; }
    }
}

