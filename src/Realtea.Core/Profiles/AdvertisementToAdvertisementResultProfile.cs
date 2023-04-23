using AutoMapper;
using Realtea.Core.Entities;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Profiles
{
	public class AdvertisementToAdvertisementResultProfile : Profile
	{
		public AdvertisementToAdvertisementResultProfile()
		{
			CreateMap<Advertisement, AdvertisementResult>()
				.ForMember(dest => dest.Price, opt => opt.MapFrom(src => src.Price.Value))
				.ForMember(dest => dest.SquareMeter, opt => opt.MapFrom(src => src.SquareMeter.Value));
		}
	}
}

