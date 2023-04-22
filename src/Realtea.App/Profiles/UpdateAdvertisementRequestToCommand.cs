using AutoMapper;
using Realtea.App.Requests.Advertisement;
using Realtea.Core.Commands.Advertisement;

namespace Realtea.App.Profiles
{
	public class UpdateAdvertisementRequestToCommandProfile : Profile
	{
		public UpdateAdvertisementRequestToCommandProfile()
		{
			CreateMap<UpdateAdvertisementRequest, UpdateAdvertisementCommand>();
		}
	}
}

