using AutoMapper;
using Realtea.Core.Entities;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Profiles
{
	public class AdvertisementToAdvertisementResultProfile : Profile
	{
		public AdvertisementToAdvertisementResultProfile()
		{
			CreateMap<Advertisement, AdvertisementResult>();
		}
	}
}

