
using System.Collections.Generic;
using Allianz.Models;
using Allianz.Models.InputModels;
using AutoMapper;

namespace OwnLand.Utilities
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            CreateMap<UserModel, User>();
            CreateMap<VehiclePurchaseModel, Application>();
        }
    }
}
