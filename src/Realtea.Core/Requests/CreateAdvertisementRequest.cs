﻿using System;
using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Responses.Advertisement;

namespace Realtea.Core.Requests
{
	public class CreateAdvertisementRequest
	{
        public string? Name { get; set; }

        public string? Description { get; set; }

        // DO NOT FORGET TO CHANGE IT.
        public AdvertisementType? AdvertisementType { get; set; }

        public CreateAdvertisementDetailsRequest? UpdateAdvertisementDetails { get; set; }

        public bool? IsActive { get; set; }
    }

    public class CreateAdvertisementDetailsRequest
    {
        public DealTypeEnum? DealType { get; set; }

        public decimal? Price { get; set; }

        public decimal? SquareMeter { get; set; }

        public LocationEnum? Location { get; set; }
    }
}
