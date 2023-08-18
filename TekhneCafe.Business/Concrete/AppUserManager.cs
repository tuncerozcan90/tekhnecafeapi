using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.DTOs.AppUser;
using TekhneCafe.DataAccess.Abstract;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class AppUserManager : IAppUserService
    {
        private readonly IAppUserDal _userDal;
        private readonly IMapper _mapper;

        public AppUserManager(IAppUserDal userDal, IMapper mapper)
        {
            _userDal = userDal;
            _mapper = mapper;
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

        public async Task CreateUserAsync(AppUser roleDto)
            => await _userDal.AddAsync(roleDto);

        public async Task UpdateUserAsync(AppUser user)
            => await _userDal.UpdateAsync(user);
    }
}
