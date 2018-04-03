using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApi.Models
{
    public class JwtResult
    {
        public DateTime createdAt { get; set; }
        public double expiresIn { get; set; }
        public string tokenType { get; set; }
        public string accessToken { get; set; }
    }
    public class AuthorizationResponse: SimpleResponse<JwtResult>
    {
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
    }
}
