using System;
using AutoMapper;
using Realtea.App.Responses.Advertisement;
using Realtea.Core.Results.Advertisement;

namespace Realtea.App.Profiles
{
	public class AdvertisementResultToReadAdvertisementResponseProfile : Profile
	{
		public AdvertisementResultToReadAdvertisementResponseProfile()
		{
			CreateMap<AdvertisementResult, ReadAdvertisementsResponse>();		
		}
	}
}

