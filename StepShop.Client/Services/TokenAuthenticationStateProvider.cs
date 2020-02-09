using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace StepShop.Client.Services
{
    public class TokenAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly TokenStorage tokenStorage;
        private readonly HttpClient httpClient;

        public TokenAuthenticationStateProvider(
            TokenStorage tokenStorage,
            HttpClient httpClient)
        {
            this.tokenStorage = tokenStorage;
            this.httpClient = httpClient;
        }

        public void StateChanged()
        {
            NotifyAuthenticationStateChanged(GetAuthenticationStateAsync());
        }

        public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {
            var token = await tokenStorage.GetAccessToken();

            if (token == null)
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
                return new AuthenticationState(new ClaimsPrincipal(new ClaimsIdentity()));
            }
            else
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var identity = new ClaimsIdentity(ParseClaimsFromJwt(token), "jwt");

                var name = identity.FindFirst("unique_name")?.Value;
                if (!String.IsNullOrWhiteSpace(name))
                    identity.AddClaim(new Claim(ClaimsIdentity.DefaultNameClaimType, identity.FindFirst("unique_name").Value));

                return new AuthenticationState(new ClaimsPrincipal(identity));
            }
        }

        private static IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            var payload = jwt.Split('.')[1];
            var jsonBytes = ParseBase64WithoutPadding(payload);
            var keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);
            return keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString()));
        }

        private static byte[] ParseBase64WithoutPadding(string base64)
        {
            switch (base64.Length % 4)
            {
                case 2: base64 += "=="; break;
                case 3: base64 += "="; break;
            }
            return Convert.FromBase64String(base64);
        }
    }
}
