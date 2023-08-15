using Microsoft.Extensions.Configuration;
using System.DirectoryServices;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TekhneCafe.Business.Abstract;
using TekhneCafe.Core.Consts;
using TekhneCafe.Core.DTOs.Authentication;
using TekhneCafe.Core.Exceptions;
using TekhneCafe.Core.Exceptions.Authentication;
using TekhneCafe.Entity.Concrete;

namespace TekhneCafe.Business.Concrete
{
    public class LdapAuthenticationManager : IAuthenticationService
    {
        private readonly string ldapPath;
        private readonly string ldapUser;
        private readonly string ldapPassword;
        private readonly ITokenService _tokenService;
        private readonly IAppUserService _userService;
        private DirectoryEntry? ldapConnection;

        public LdapAuthenticationManager(IConfiguration configuration, ITokenService tokenService, IAppUserService userService)
        {
            ldapPath = configuration.GetSection("LdapSettings:Path").Value;
            ldapUser = configuration.GetSection("LdapSettings:Username").Value;
            ldapPassword = configuration.GetSection("LdapSettings:Password").Value;
            _tokenService = tokenService;
            _userService = userService;
        }

        public async Task<JwtResponse> Login(string username, string password)
        {
            using (ldapConnection = new DirectoryEntry(ldapPath, ldapUser, ldapPassword))
            {
                // Connect to the LDAP server using admin credentials
                ldapConnection.AuthenticationType = AuthenticationTypes.Secure;
                using (var searcher = new DirectorySearcher(ldapConnection))
                {
                    searcher.Filter = $"(&(objectClass=user)(sAMAccountName={username}))";
                    SearchResult searchResult;
                    try
                    {
                        searchResult = searcher.FindOne();

                    }
                    catch (Exception ex)
                    {
                        throw new InternalServerErrorException(ex.Message);
                    }

                    return searchResult != null
                        ? await AuthenticateUser(searchResult.Path, username, password)
                        : throw new UserNotFoundException();
                }
            }
        }

        private async Task<JwtResponse> AuthenticateUser(string userDN, string username, string password)
        {
            using (var userEntry = new DirectoryEntry(userDN, username, password))
            {
                userEntry.AuthenticationType = AuthenticationTypes.Secure;
                try
                {
                    return await AuthorizeUser(userEntry, userDN);
                }
                catch (Exception ex)
                {
                    throw new UserBadRequestException(ex.Message);
                }
            }
        }

        private async Task<JwtResponse> AuthorizeUser(DirectoryEntry userEntry, string userDN)
        {
            string groupToCheck = "CN=Cafe Admin";
            AppUser user = await CheckUserExistsInDatabase(userEntry);
            using (var groupSearcher = new DirectorySearcher(ldapConnection))
            {
                SearchResult groupResult = CheckUserIsInGroup(groupSearcher, groupToCheck, userDN);
                List<Claim> claims;
                if (groupResult is null)
                {
                    groupToCheck = "CN=Cafe Service";
                    groupResult = CheckUserIsInGroup(groupSearcher, groupToCheck, userDN);
                    claims = SetClaims(groupResult != null ? RoleConsts.CafeService : RoleConsts.CafeUser, user);
                }
                else
                    claims = SetClaims(RoleConsts.CafeAdmin, user);
                var token = _tokenService.GenerateToken(claims);

                return new JwtResponse
                {
                    Token = new JwtSecurityTokenHandler().WriteToken(token),
                    ValidTo = token.ValidTo
                };
            }
        }

        private SearchResult CheckUserIsInGroup(DirectorySearcher groupSearcher, string groupToCheck, string userDN)
        {
            groupSearcher.Filter = $"(&(objectClass=group)({groupToCheck})(member={userDN.Replace($"{ldapPath}/", "")}))";
            return groupSearcher.FindOne();
        }

        private async Task<AppUser> CheckUserExistsInDatabase(DirectoryEntry userEntry)
        {
            Guid ldapId = userEntry.Guid;
            AppUser user = await _userService.GetUserByLdapIdAsync(ldapId.ToString());
            if (user != null)
                return user;
            user = new()
            {
                Id = Guid.NewGuid(),
                LdapId = ldapId,
                FullName = userEntry.Properties["displayname"].Value.ToString(),
                Username = userEntry.Properties["sAMAccountName"].Value.ToString(),
                Email = userEntry.Properties["mail"].Value.ToString(),
                Department = userEntry.Properties["department"].Value.ToString(),
                Phone = userEntry.Properties["telephoneNumber"].Value.ToString()
            };
            await _userService.CreateUserAsync(user);

            return user;
        }

        private List<Claim> SetClaims(string roleName, AppUser user)
            => new List<Claim>()
            {
                new Claim(ClaimTypes.Role, roleName),
                new Claim(ClaimTypes.Name, user.FullName),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };
    }
}