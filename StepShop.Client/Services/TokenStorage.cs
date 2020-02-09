using Blazor.Extensions.Storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StepShop.Client.Services
{
    public class TokenStorage
    {
        private readonly ILocalStorage localStorage;

        public TokenStorage(
            ILocalStorage localStorage)
        {
            this.localStorage = localStorage;
        }

        public async Task SetTokensAsync(string accessToken, string refreshToken)
        {
            await localStorage.SetItem("accessToken", accessToken);
            await localStorage.SetItem("refreshToken", refreshToken);
        }

        public async Task<string> GetAccessToken()
        {
            return await localStorage.GetItem<string>("accessToken");
        }

        public async Task<string> GetRefreshToken()
        {
            return await localStorage.GetItem<string>("refreshToken");
        }

        public async Task RemoveTokens()
        {
            await localStorage.RemoveItem("accessToken");
            await localStorage.RemoveItem("refreshToken");
        }
    }
}
