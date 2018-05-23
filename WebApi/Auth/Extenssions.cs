using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace WebApi.Auth
{
    public static class Extenssions
    {
        public static long GetId (this IPrincipal principal) {
            long id;
            return long.TryParse(principal.Identity.GetUserId(), out id) ? id : 0;
        }
    }
}
