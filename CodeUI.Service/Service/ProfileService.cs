using AutoMapper;
using CodeUI.Data.UnitOfWork;
using CodeUI.Service.DTO.Request.ProfileRequest;
using CodeUI.Service.DTO.Response;
using FirebaseAdmin.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeUI.Service.Service
{
    public interface IProfileService
    {
        Task<BaseResponseViewModel<ProfileResponse>> UpdateProfile(int profileId, UpdateProfileRequest request);
    }
    public class ProfileService : IProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProfileService(IUnitOfWork unitOfWork, IMapper mapper) 
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BaseResponseViewModel<ProfileResponse>> UpdateProfile(int profileId, UpdateProfileRequest request)
        {
            try
            {
                var profile = _unitOfWork.Repository<Data.Entity.Profile>().GetAll().FirstOrDefault(x => x.Id == profileId);

                if(profile == null)
                {
                    throw new Exception();
                }

                var updatedProfile = _mapper.Map<UpdateProfileRequest,Data.Entity.Profile>(request,profile);

                await _unitOfWork.Repository<Data.Entity.Profile>().UpdateDetached(updatedProfile);
                await _unitOfWork.CommitAsync();

                return new BaseResponseViewModel<ProfileResponse>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Success",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<ProfileResponse>(updatedProfile)
                };
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
