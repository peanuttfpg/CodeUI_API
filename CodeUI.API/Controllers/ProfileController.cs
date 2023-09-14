using CodeUI.Service.DTO.Request.ProfileRequest;
using CodeUI.Service.DTO.Response;
using CodeUI.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeUI.API.Controllers
{
    [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class ProfileController : ControllerBase
    {
        private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
        }
        ///<summary>
        /// Update a profile by id
        /// </summary>
        [HttpPut("updateById")]
        public async Task<ActionResult<BaseResponseViewModel<ProfileResponse>>> UpdateProfile(int profileId, UpdateProfileRequest request)
        {
            try
            {
                var result = await _profileService.UpdateProfile(profileId, request);
                return Ok(result);
            } 
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
