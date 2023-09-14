using AutoMapper;
using AutoMapper.QueryableExtensions;
using Castle.Core.Resource;
using CodeUI.Data.Entity;
using CodeUI.Data.UnitOfWork;
using CodeUI.Service.Attributes;
using CodeUI.Service.DTO.Request;
using CodeUI.Service.DTO.Request.AccountRequest;
using CodeUI.Service.DTO.Request.ProfileRequest;
using CodeUI.Service.DTO.Response;
using CodeUI.Service.Helpers;
using CodeUI.Service.Utilities;
using FirebaseAdmin.Auth;
using Hangfire.MemoryStorage.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CodeUI.Service.Helpers.Enum;

namespace CodeUI.Service.Service
{
    public interface IAccountService
    {
        Task<BaseResponseViewModel<AccountResponse>> GetAccountById(int accountId);
        Task<BaseResponsePagingViewModel<AccountResponse>> GetAccounts(AccountResponse filter, PagingRequest paging);
        Task<BaseResponseViewModel<AccountResponse>> CreateAccount(CreateAccountRequest request);
        Task<BaseResponseViewModel<LoginResponse>> LoginByMail(ExternalAuthRequest request);
        Task<BaseResponseViewModel<LoginResponse>> LoginByPassword(LoginRequest request);
    }
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public AccountService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<BaseResponseViewModel<AccountResponse>> CreateAccount(CreateAccountRequest request)
        {
            try
            {

                var newAccount = new Account()
                {
                    Username = request.Name,
                    Email = request.Email,
                    Password= request.Password,
                    Profile = new Data.Entity.Profile
                    {
                        ImageUrl = request.CreateProfile.ImageUrl,
                        Phone = request.CreateProfile.Phone
                    },
                    RoleId = (int)AccountTypeEnum.Creator,
                    IsActive = true,
                    CreateDate = DateTime.Now
                };
                await _unitOfWork.Repository<Account>().InsertAsync(newAccount);

                await _unitOfWork.CommitAsync();

                return new BaseResponseViewModel<AccountResponse>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Success",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<AccountResponse>(newAccount)
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<LoginResponse>> LoginByMail(ExternalAuthRequest request)
        {
            try
            {
                string newAccessToken = null;

                //decode token -> user record
                var auth = FirebaseAdmin.Auth.FirebaseAuth.DefaultInstance;
                FirebaseToken decodeToken = await auth.VerifyIdTokenAsync(request.IdToken);
                UserRecord userRecord = await auth.GetUserAsync(decodeToken.Uid);

                //check exist customer 
                var account = _unitOfWork.Repository<Account>().GetAll()
                                .FirstOrDefault(x => x.Email.Contains(userRecord.Email));

                //new customer => add fcm map with Id
                if (account is null)
                {
                    CreateAccountRequest newAccount = new CreateAccountRequest()
                    {
                        Name = userRecord.DisplayName,
                        Email = userRecord.Email,
                        CreateProfile = new CreateProfileRequest
                        {
                            ImageUrl = userRecord.PhotoUrl,
                            Phone = userRecord.PhoneNumber
                        }
                    };

                    //create customer
                    var customerResult = CreateAccount(newAccount).Result.Data;
                    account = _mapper.Map<Account>(customerResult);
                }
                newAccessToken = AccessTokenManager.GenerateJwtToken(string.IsNullOrEmpty(account.Username) ? "" : account.Username, null, Guid.NewGuid(), _configuration);

                return new BaseResponseViewModel<LoginResponse>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Success",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = new LoginResponse()
                    {
                        access_token = newAccessToken,
                        account = _mapper.Map<AccountResponse>(account)
                    }
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public async Task<BaseResponseViewModel<LoginResponse>> LoginByPassword(LoginRequest request)
        {
            try
            {
                string newAccessToken = null;

                //check exist customer 
                var account = _unitOfWork.Repository<Account>().GetAll()
                                .FirstOrDefault(x => x.Username.Contains(request.Username));

                //new customer => add fcm map with Id
                if (account is null)
                {
                    CreateAccountRequest newAccount = new CreateAccountRequest()
                    {
                        Name = request.Username,
                        Password= request.Password,
                    };

                    //create customer
                    var customerResult = CreateAccount(newAccount).Result.Data;
                    account = _mapper.Map<Account>(customerResult);
                }
                newAccessToken = AccessTokenManager.GenerateJwtToken(string.IsNullOrEmpty(account.Username) ? "" : account.Username, null, Guid.NewGuid(), _configuration);

                return new BaseResponseViewModel<LoginResponse>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Success",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = new LoginResponse()
                    {
                        access_token = newAccessToken,
                        account = _mapper.Map<AccountResponse>(account)
                    }
                };
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public async Task<BaseResponsePagingViewModel<AccountResponse>> GetAccounts(AccountResponse filter, PagingRequest paging)
        {
            try
            {
                var customer = _unitOfWork.Repository<Account>().GetAll()
                    .ProjectTo<AccountResponse>(_mapper.ConfigurationProvider)
                    .DynamicFilter(filter)
                    .DynamicSort(filter)
                    .PagingQueryable(paging.Page, paging.PageSize, Constants.LimitPaging, Constants.DefaultPaging);

                return new BaseResponsePagingViewModel<AccountResponse>()
                {
                    Metadata = new PagingsMetadata()
                    {
                        Page = paging.Page,
                        Size = paging.PageSize,
                        Total = customer.Item1
                    },
                    Data = customer.Item2.ToList()
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<BaseResponseViewModel<AccountResponse>> GetAccountById(int id)
        {
            try
            {
                var account = await _unitOfWork.Repository<Account>().GetAll()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                //if (account == null)
                //    throw new ErrorResponse(404, (int)CustomerErrorEnums.NOT_FOUND,
                //        CustomerErrorEnums.NOT_FOUND.GetDisplayName());

                return new BaseResponseViewModel<AccountResponse>()
                {
                    Status = new StatusViewModel()
                    {
                        Message = "Success",
                        Success = true,
                        ErrorCode = 0
                    },
                    Data = _mapper.Map<AccountResponse>(account)
                };
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}


