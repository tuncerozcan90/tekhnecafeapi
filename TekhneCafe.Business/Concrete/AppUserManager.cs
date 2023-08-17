using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.AppUser;
using TekhneCafe.Core.Exceptions.AppUser;
using TekhneCafe.Core.Exceptions.Order;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.DataAccess.Concrete.EntityFramework;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserDal _userDal;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;

        public AppUserManager(IAppUserDal userDal, IMapper mapper, IHttpContextAccessor httpContext)
        {
            _userDal = userDal;
            _mapper = mapper;
            _httpContext = httpContext;
        }

        public List<AppUserListDto> GetUserList()
        {
            var users = _userDal.GetAll();
            return _mapper.Map<List<AppUserListDto>>(users);
        }

        public async Task GetUserByIdAsnyc(string id)
        {
            AppUser user = await _userDal.GetByIdAsync(Guid.Parse(id));
        }

        public async Task<AppUser> GetUserByLdapIdAsync(string id)
            => await _userDal.GetAll(_ => _.LdapId == Guid.Parse(id)).FirstOrDefaultAsync();

        public async Task<AppUser> GetUserByIdAsync(string id) 
        {
            AppUser appUser = await _userDal.GetAll(_ => _.Id == Guid.Parse(id)).FirstOrDefaultAsync();

            if (appUser is null)
                throw new AppUserNotFoundException();

            return appUser;
        }

        public async Task CreateUserAsync(AppUser roleDto)
            => await _userDal.AddAsync(roleDto);
    }
}
