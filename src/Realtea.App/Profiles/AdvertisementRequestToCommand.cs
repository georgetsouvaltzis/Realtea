using System;
using AutoMapper;
using Realtea.App.Requests.Advertisement;
using Realtea.Core.Commands.Advertisement;

namespace Realtea.App.Profiles
{
	public class AdvertisementRequestToCommandProfile : Profile
	{
		public AdvertisementRequestToCommandProfile()
		{
			CreateMap<CreateAdvertisementRequest, CreateAdvertisementCommand>();
		}
	}
}

