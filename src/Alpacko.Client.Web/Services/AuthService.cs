using Alpacko.Client.Web.Models;
using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components.Authorization;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text;
using System.Net.Http.Headers;

namespace Alpacko.Client.Web.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;
        private readonly AuthenticationStateProvider _authenticationStateProvider;
        private readonly ILocalStorageService _localStorage;

        public AuthService(HttpClient httpClient,
                           AuthenticationStateProvider authenticationStateProvider,
                           ILocalStorageService localStorage)
        {
            _httpClient = httpClient;
            _authenticationStateProvider = authenticationStateProvider;
            _localStorage = localStorage;
        }

        public async Task<SignInResultModel> SignIn(SignInModel signInModel)
        {
            var response = await _httpClient.PostAsync("api/signin", JsonContent.Create<SignInModel>(signInModel));

            SignInResultModel loginResult = JsonSerializer.Deserialize<SignInResultModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
                return loginResult;

            if (signInModel.StaySignedIn)
                await _localStorage.SetItemAsync("authToken", loginResult.Token);

            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsAuthenticated(loginResult.Token);
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", loginResult.Token);

            return loginResult;
        }

        public async Task SignOut()
        {
            await _localStorage.RemoveItemAsync("authToken");
            ((ApiAuthenticationStateProvider)_authenticationStateProvider).MarkUserAsLoggedOut();
            _httpClient.DefaultRequestHeaders.Authorization = null;
        }

        public async Task<SignUpResultModel> SignUp(SignUpModel signUpModel)
{
            HttpResponseMessage response = await _httpClient.PostAsJsonAsync("api/signup", signUpModel);
            SignUpResultModel signUpResult = JsonSerializer.Deserialize<SignUpResultModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            //SignUpResultModel signUpResult = await result.Content.ReadFromJsonAsync<SignUpResultModel>();

            return signUpResult;
        }
    }
}
