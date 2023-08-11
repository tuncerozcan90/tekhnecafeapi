using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.DataAccess.Abstract;
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
        public async Task<AppUser> GetUserByLdapIdAsync(string id)
            => await _userDal.GetAll(_ => _.LdapId == Guid.Parse(id)).FirstOrDefaultAsync();

        public async Task CreateUserAsync(AppUser roleDto)
            => await _userDal.AddAsync(roleDto);
    }
}
