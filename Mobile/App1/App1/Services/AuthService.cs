using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using App1.ApiDTO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace App1.Services
{
    public class AuthenticationService {
        private Http _httpClient;
        public AuthenticationService() {
            this._httpClient=new Http();
        }
        public async Task<AuthResult> Authenticate(string login,string password) {
            var request = new AuthorizationRequest {
                Password = password,
                UserName = login
            };
            var response = await this._httpClient.PostAsJson<AuthorizationResponse>("Account/Authenticate", request);
            if (response.IsSuccess) {
                return new AuthResult
                {
                    IsSuccess = true,
                    Token = response.Data
                };
            }
            return new AuthResult
            {
                IsSuccess = false,
                ErrorText = response.ErrorText
            };
        }
    }

    public class AuthResult {
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
        public JwtResult Token { get; set; }
    }
    public class JwtResult
    {
        public DateTime createdAt { get; set; }
        public double expiresIn { get; set; }
        public string tokenType { get; set; }
        public string accessToken { get; set; }
    }
}
