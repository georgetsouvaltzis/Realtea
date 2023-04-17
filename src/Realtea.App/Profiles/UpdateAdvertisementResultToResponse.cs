using System;
using AutoMapper;
using Realtea.App.Responses.Advertisement;
using Realtea.Core.Results.Advertisement;

namespace Realtea.App.Profiles
{
	public class UpdateAdvertisementResultToResponseProfile : Profile
	{
		public UpdateAdvertisementResultToResponseProfile()
		{
			CreateMap<UpdateAdvertisementResult, UpdateAdvertisementResponse>();
		}
	}
}

