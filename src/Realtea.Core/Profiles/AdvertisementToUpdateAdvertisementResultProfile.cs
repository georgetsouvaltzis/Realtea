using AutoMapper;
using Realtea.Core.Entities;
using Realtea.Core.Results.Advertisement;

namespace Realtea.Core.Profiles
{
	public class AdvertisementToUpdateAdvertisementResultProfile : Profile
	{
		public AdvertisementToUpdateAdvertisementResultProfile()
		{
			CreateMap<Advertisement, UpdateAdvertisementResult>()
				.ForMember(x => x.SquareMeter, opt => opt.MapFrom(src => src.SquareMeter.Value))
				.ForMember(x => x.Price, opt => opt.MapFrom(src => src.Price.Value));
		}
	}
}