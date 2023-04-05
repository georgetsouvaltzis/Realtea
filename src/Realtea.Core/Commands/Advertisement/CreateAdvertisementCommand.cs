using System;
using MediatR;
using Realtea.Core.Enums;
using Realtea.Core.Requests;
using Realtea.Core.Responses.Advertisement;

namespace Realtea.Core.Commands.Advertisement
{
	public class CreateAdvertisementCommand : IRequest<CreateAdvertisementResponse>
	{
        public int UserId { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        // DO NOT FORGET TO CHANGE IT.
        public AdvertisementType? AdvertisementType { get; set; }

        public CreateAdvertisementDetailsRequest? UpdateAdvertisementDetails { get; set; }

        public bool? IsActive { get; set; }
    }
}

