using AutoMapper;
using CodeUI.Data.Entity;
using CodeUI.Service.DTO.Request.ProfileRequest;
using CodeUI.Service.DTO.Response;

namespace CodeUI.API.Mapper
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile()
        {
            #region Mapping Template
            //CreateMap<CurrentObject, DestinationObject>().ReverseMap();
            #endregion

            #region Account
            CreateMap<Account, AccountResponse>().ReverseMap();
            #endregion

            #region Profile
            CreateMap<Data.Entity.Profile,UpdateProfileRequest>().ReverseMap();
            CreateMap<Data.Entity.Profile,ProfileResponse>().ReverseMap();
            #endregion
        }
    }
}
