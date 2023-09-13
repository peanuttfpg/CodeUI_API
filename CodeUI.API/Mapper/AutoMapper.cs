using AutoMapper;
using CodeUI.Data.Entity;
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
        }
    }
}
