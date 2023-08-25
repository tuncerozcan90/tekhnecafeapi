using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Business.Helpers.FilterServices;
using TekhneCafe.Business.Helpers.HeaderServices;
using TekhneCafe.Core.DTOs.AppUser;
using TekhneCafe.Core.Exceptions;
using TekhneCafe.Core.Exceptions.AppUser;
using TekhneCafe.Core.Extensions;
using TekhneCafe.Core.Filters.AppUser;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Helpers.Transaction;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly ITransactionManagement _transactionManagement;

        public AppUserManager(IAppUserDal userDal, IMapper mapper, IHttpContextAccessor httpContextAccessor, ITransactionManagement transactionManagement)
        {
            _userDal = userDal;
            _mapper = mapper;
            _httpContext = httpContextAccessor;
            _transactionManagement = transactionManagement;
        }

        public List<AppUserListDto> GetUserList(AppUserRequestFilter filters = null)
        {
            var users = _userDal.GetAll();
            var filteredResult = new AppUserFilterService().FilterAppUsers(users, filters);
            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
            return _mapper.Map<List<AppUserListDto>>(filteredResult.ResponseValue);
        }

        public async Task<AppUser> GetUserByLdapIdAsync(string id)
            => await _userDal.GetAll(_ => _.LdapId == Guid.Parse(id)).FirstOrDefaultAsync();

        public async Task<AppUser> GetRawUserByIdAsync(string id)
        {
            AppUser appUser = await _userDal.GetByIdAsync(Guid.Parse(id));
            ThrowExceptionUserNotExists(appUser);
            return appUser;
        }

        public async Task<AppUserListDto> GetUserByIdAsync(string id)
        {
            AppUser appUser = await _userDal.GetByIdAsync(Guid.Parse(id));
            ThrowExceptionUserNotExists(appUser);
            return _mapper.Map<AppUserListDto>(appUser);
        }

        public async Task<AppUser> CreateUserAsync(AppUserAddDto userDto)
        {
            //todo: user validation required
            AppUser user = _mapper.Map<AppUser>(userDto);
            using (var transaction = await _transactionManagement.BeginTransactionAsync())
            {
                try
                {
                    await _userDal.AddAsync(user);
                    await _transactionManagement.CommitTransactionAsync();
                }
                catch (Exception ex)
                {
                    throw new InternalServerErrorException();
                }
            }

            return user;
        }

        public async Task UpdateUserPhoneAsync(string phone)
        {
            Guid userId = Guid.Parse(_httpContext.HttpContext.User.ActiveUserId());
            AppUser user = await _userDal.GetByIdAsync(userId);
            user.Phone = phone;
            await _userDal.UpdateAsync(user);
        }

        public async Task UpdateUserAsync(AppUser user)
            => await _userDal.UpdateAsync(user);

        public void ThrowExceptionUserNotExists(AppUser user)
        {
            if (user is null)
                throw new AppUserNotFoundException();
        }
    }
}
