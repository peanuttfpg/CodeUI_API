using CodeUI.Service.DTO.Request;
using CodeUI.Service.DTO.Request.AccountRequest;
using CodeUI.Service.DTO.Response;
using CodeUI.Service.Service;
using Microsoft.AspNetCore.Mvc;

namespace CodeUI.API.Controllers
{
    [Route(Helpers.SettingVersionApi.ApiVersion)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
            //
        }

        /// <summary>
        /// Google Login
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        [HttpPost("loginByMail")]
        public async Task<ActionResult<BaseResponseViewModel<AccountResponse>>> LoginByMail([FromBody] ExternalAuthRequest data)
        {
            try
            {
                var result = await _accountService.LoginByMail(data);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Login via password
        /// </summary>
        [HttpPost("login")]
        public async Task<ActionResult<BaseResponseViewModel<AccountResponse>>> LoginByPassword(LoginRequest request)
        {
            try
            {
                var result = await _accountService.LoginByPassword(request);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        ///<summary>
        /// Register a new account
        ///</summary>
        ///<param name="request"></param>
        ///<returns></returns>
        [HttpPost("register")]
        public async Task<ActionResult<BaseResponseViewModel<AccountResponse>>> Register([FromBody] CreateAccountRequest request)
        {
            try
            {
                var result = await _accountService.CreateAccount(request);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        ///<summary>
        /// Get all accounts with filter
        /// </summary>
        [HttpGet("getAll")]
        public async Task<ActionResult<BaseResponsePagingViewModel<AccountResponse>>> GetAll([FromQuery]AccountResponse filter, [FromQuery] PagingRequest paging)
        {
            try
            {
                var result = await _accountService.GetAccounts(filter, paging);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
