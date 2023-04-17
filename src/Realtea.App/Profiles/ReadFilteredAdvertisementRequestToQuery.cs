using System;
using AutoMapper;
using Realtea.App.Requests.Advertisement;
using Realtea.Core.Queries;

namespace Realtea.App.Profiles
{
	public class ReadFilteredAdvertisementRequestToQueryProfile : Profile
	{
		public ReadFilteredAdvertisementRequestToQueryProfile()
		{
			CreateMap<ReadFilteredAdvertisementRequest, ReadFilteredAdvertisementsQuery>();
		}
	}
}

