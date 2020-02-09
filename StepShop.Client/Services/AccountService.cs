using Microsoft.AspNetCore.Components;
using StepShop.Client.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace StepShop.Client.Services
{
    public class AccountService
    {
        private readonly TokenStorage tokenStorage;
        private readonly HttpClient httpClient;
        private readonly TokenAuthenticationStateProvider authState;

        private readonly string authApiUrl = "http://localhost:5000/api/authentication";

        public AccountService(
            TokenStorage tokenStorage,
            HttpClient httpClient,
            TokenAuthenticationStateProvider authState)
        {
            this.tokenStorage = tokenStorage;
            this.httpClient = httpClient;
            this.authState = authState;
        }

        public async Task Login(LoginCredentialsDto credentials)
        {
            var response = await httpClient.PostJsonAsync<AuthenticationResponseDto>($"{authApiUrl}/login", credentials);
            await tokenStorage.SetTokensAsync(response.AccessToken, response.RefreshToken);
            authState.StateChanged();
        }

        public async Task Logout()
        {
            var refreshToken = await tokenStorage.GetRefreshToken();
            await httpClient.GetAsync($"{authApiUrl}/logout/{refreshToken}");
            await tokenStorage.RemoveTokens();
            authState.StateChanged();
        }
    }
}
