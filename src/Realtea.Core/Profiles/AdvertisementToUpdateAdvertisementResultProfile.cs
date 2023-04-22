using System;
using AutoMapper;
using Realtea.Core.Entities;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Profiles
{
	public class AdvertisementToUpdateAdvertisementResultProfile : Profile
	{
		public AdvertisementToUpdateAdvertisementResultProfile()
		{
			CreateMap<Advertisement, UpdateAdvertisementResult>();
		}
	}
}

