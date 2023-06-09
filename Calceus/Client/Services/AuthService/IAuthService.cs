﻿using Calceus.Shared;

namespace Calceus.Client.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>?> Register(UserRegister user);

        Task<ServiceResponse<string>> Login(UserLogin user);

        Task<bool> IsUserAuthenticated();
    }
}
