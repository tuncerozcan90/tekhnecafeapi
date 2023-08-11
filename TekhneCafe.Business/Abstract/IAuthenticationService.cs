﻿using TekhneCafe.Core.DTOs.Authentication;

namespace TekhneCafe.Business.Abstract
{
    public interface IAuthenticationService
    {
        Task<JwtResponse> Login(string email, string password);
        //Task Login2(string email, string password);
    }
}
