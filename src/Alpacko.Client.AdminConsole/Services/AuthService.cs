using Alpacko.Client.AdminConsole.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using System.Threading.Tasks;

namespace Alpacko.Client.AdminConsole.Services
{
    public delegate void AuthenticationStateChanged();

    public class AuthService
    {
        public int? SignInUserId => signedInUser?.Id;
        public string SignInUserEmail => signedInUser?.Email;
        public string SignInUserRole => signedInUser?.Role;
        public IEnumerable<Claim> SignInUserClaims => signedInUser?.Claims;
        public bool UserIsAuthenticated => signedInUser != null;

        public event AuthenticationStateChanged UserSigndInEvent;

        public event AuthenticationStateChanged UserSignedOutEvent;

        private User signedInUser;
        private HttpClient httpClient;

        public AuthService(HttpClient client)
        {
            httpClient = client;
        }

        public bool UserHasRole(string role)
        {
            return SignInUserRole == role;
        }

        public bool UserHasRole(IEnumerable<string> anyRole)
        {
            return anyRole.Contains(SignInUserRole);
        }

        public async Task<SignInResultModel> SignIn(SignInModel signInModel)
        {
            HttpResponseMessage response = await httpClient.PostAsync("api/signin", JsonContent.Create(signInModel));
            SignInResultModel signInResult = JsonSerializer.Deserialize<SignInResultModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            if (!response.IsSuccessStatusCode)
                return signInResult;

            MarkUserAsAuthenticated(signInResult.Token);

            return signInResult;
        }

        public void MarkUserAsAuthenticated(string token)
        {
            signedInUser = new User(ParseClaimsFromJwt(token), token);
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);

            UserSigndInEvent?.Invoke();
        }

        public void SignOut()
        {
            signedInUser = null;
            httpClient.DefaultRequestHeaders.Authorization = null;
            UserSignedOutEvent?.Invoke();
        }

        public async Task<SignUpResultModel> SignUp(SignUpModel signUpModel)
        {
            HttpResponseMessage response = await httpClient.PostAsJsonAsync("api/signup", signUpModel);
            SignUpResultModel signUpResult = JsonSerializer.Deserialize<SignUpResultModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return signUpResult;
        }

        public async Task<SignUpResultModel> SignUp(SignUpModel signUpModel, int roleId, int? postofficeId)
        {
            string requestUri = $"api/signup/{roleId}" + (postofficeId is null ? "" : $"?postofficeid={postofficeId.Value}");
            HttpResponseMessage response = await httpClient.PostAsJsonAsync(requestUri, signUpModel);
            SignUpResultModel signUpResult = JsonSerializer.Deserialize<SignUpResultModel>(await response.Content.ReadAsStringAsync(), new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return signUpResult;
        }

        private IEnumerable<Claim> ParseClaimsFromJwt(string jwt)
        {
            List<Claim> claims = new List<Claim>();
            string payload = jwt.Split('.')[1];
            byte[] jsonBytes = ParseBase64WithoutPadding(payload);
            Dictionary<string, object> keyValuePairs = JsonSerializer.Deserialize<Dictionary<string, object>>(jsonBytes);

            keyValuePairs.TryGetValue(ClaimTypes.Role, out object roles);

            if (roles != null)
            {
                if (roles.ToString().Trim().StartsWith("["))
                {
                    string[] parsedRoles = JsonSerializer.Deserialize<string[]>(roles.ToString());

                    foreach (string parsedRole in parsedRoles)
                    {
                        claims.Add(new Claim(ClaimTypes.Role, parsedRole));
                    }
                }
                else
                {
                    claims.Add(new Claim(ClaimTypes.Role, roles.ToString()));
                }

                keyValuePairs.Remove(ClaimTypes.Role);
            }

            claims.AddRange(keyValuePairs.Select(kvp => new Claim(kvp.Key, kvp.Value.ToString())));

            return claims;
        }

        private byte[] ParseBase64WithoutPadding(string base64)
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