using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using App1.Services;

namespace App1.ApiDTO
{
    public class AuthorizationRequest {
        public string UserName { get; set; }
        public string Password { get; set; }
    }

    public class AuthorizationResponse:SimpleResponse<JwtResult> {
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
    }

    public class SimpleResponse {
        public Object Data { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorText { get; set; }
        public HttpStatusCode Code { get; set; }
    }
    public class SimpleResponse<T> : SimpleResponse
    {
        public T Data { get; set; }
    }
}
