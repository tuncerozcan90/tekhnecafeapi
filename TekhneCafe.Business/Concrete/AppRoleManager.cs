//using AutoMapper;
//using Microsoft.AspNetCore.Http;
//using TekhneCafe.Business.Abstract;
//using TekhneCafe.Business.Helpers.FilterServices;
//using TekhneCafe.Business.Helpers.HeaderServices;
//using TekhneCafe.Core.DTOs.AppRole;
//using TekhneCafe.Core.Exceptions.AppRole;
//using TekhneCafe.Core.Filters.AppRole;
//using TekhneCafe.DataAccess.Abstract;
//using TekhneCafe.Entity.Concrete;

//namespace TekhneCafe.Business.Concrete
//{
//    public class AppRoleManager : IAppRoleService
//    {
//        private readonly IAppRoleDal _roleDal;
//        private readonly IMapper _mapper;
//        private readonly IHttpContextAccessor _httpContext;

//        public AppRoleManager(IAppRoleDal roleDal, IMapper mapper, IHttpContextAccessor httpContext)
//        {
//            _roleDal = roleDal;
//            _mapper = mapper;
//            _httpContext = httpContext;
//        }

//        public List<AppRoleListDto> GetRoles(AppRoleRequestFilter filters = null)
//        {
//            var filteredResult = new AppRoleFilterService().FilterRoles(_roleDal.GetAll(_ => _.IsValid), filters);
//            new HeaderService(_httpContext).AddToHeaders(filteredResult.Headers);
//            return _mapper.Map<List<AppRoleListDto>>(filteredResult.ResponseValue);
//        }

//        public async Task<AppRoleListDto> GetRoleByIdAsync(string id)
//        {
//            AppRole role = await GetRoleById(id);
//            return _mapper.Map<AppRoleListDto>(role);
//        }

//        public async Task CreateRoleAsync(AppRoleAddDto roleDto)
//        {
//            AppRole existingRole = await _roleDal.GetRoleByNameAsync(roleDto.Name);
//            if (existingRole != null)
//                throw new RoleAlreadyExistsException();

//            AppRole role = _mapper.Map<AppRole>(roleDto);
//            await _roleDal.AddAsync(role);
//        }

//        public async Task RemoveRoleAsync(string id)
//        {
//            AppRole role = await GetRoleById(id);
//            role.IsValid = false;
//            await _roleDal.SafeDeleteAsync(role);
//        }

//        public async Task UpdateRoleAsync(AppRoleUpdateDto roleDto)
//        {
//            AppRole role = await GetRoleById(roleDto.Id);
//            _mapper.Map(roleDto, role);
//            await _roleDal.UpdateAsync(role);
//        }

//        private async Task<AppRole> GetRoleById(string id)
//        {
//            AppRole role = await _roleDal.GetByIdAsync(Guid.Parse(id));
//            if (role is null)
//                throw new RoleNotFoundException();

//            return role;
//        }
//    }
//}
