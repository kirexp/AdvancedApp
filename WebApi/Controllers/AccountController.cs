using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;
using DAL.Entities.Account;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WebApi.Auth;
using WebApi.Auth.Models;

namespace WebApi.Controllers
{
    public class Model {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class AccountController : Controller
    {
        private SignInManager<User> SignInManager { get; }
        private ApplicationUserManager<User> UserManager { get; }
        private IConfiguration Configuration { get; }
        private readonly Repository<User> _signInRepository;
        public AccountController(ApplicationUserManager<User>userManager, ApplicationSignInManger<User> signInManager, IConfiguration configuration, SignInRepository rep)
        {
            this.SignInManager = signInManager;
            this.Configuration = configuration;
            this.UserManager = userManager;
            this._signInRepository = rep.Repository;
        }
        [HttpPost]
        public async Task<IActionResult> Authenticate([FromBody]Model model){
            using (var repository = new Repository<User>(this._signInRepository)) {
                var user = repository.Get(x => x.UserName == model.UserName).SingleOrDefault();
                repository.Dispose();
                var result = await this.SignInManager.PasswordSignInAsync(user, model.Password, false, false);
                if (result.Succeeded) {
                    var requestAt = DateTime.Now;
                    var expiresIn = requestAt + TokenAuthOption.ExpiresSpan;
                    var token = await this.GenerateToken(user, expiresIn);
                    return this.Json(new RequestResult {
                        State = RequestState.Success,
                        Data = new {
                            requestAt = requestAt,
                            expiresIn = TokenAuthOption.ExpiresSpan.TotalSeconds,
                            tokenType = TokenAuthOption.TokenType,
                            accessToken = token
                        }
                    });
                } else {
                    return null;
                }
            }
        }
        [NonAction]
        private async Task<string> GenerateToken(User user, DateTime expires)
        {
            //var claims = new List<Claim>(new[]
            //{
            //    // Issuer
            //    new Claim(JwtRegisteredClaimNames.Iss, TokenAuthOption.Issuer),   

            //    // UserName
            //    new Claim(JwtRegisteredClaimNames.Sub, user.UserName),       

            //    // Email is unique
            //    new Claim(JwtRegisteredClaimNames.Email, user.Email),        
            //});
            //var roleClaim1 = new Claim(ClaimTypes.Role, "Admin1", ClaimValueTypes.String, TokenAuthOption.Issuer);
            //var roleClaim2 = new Claim(ClaimTypes.Role, "Admin2", ClaimValueTypes.String, TokenAuthOption.Issuer);
            //var roleClaim3 = new Claim(ClaimTypes.Role, "Admin3", ClaimValueTypes.String, TokenAuthOption.Issuer);
            //claims.Add(roleClaim1);
            //claims.Add(roleClaim2);
            //claims.Add(roleClaim3);
            //var jwt = new JwtSecurityToken(
            //    issuer: TokenAuthOption.Issuer,
            //    audience: TokenAuthOption.Audience,
            //    claims: claims,
            //    expires: expires,
            //    signingCredentials: TokenAuthOption.SigningCredentials);
            //// Serialize token
            //var result = new JwtSecurityTokenHandler().WriteToken(jwt);
            //return result;

























            var handler = new JwtSecurityTokenHandler();
            var so = new List<Claim>(new[] {
                new Claim("Id", user.Id.ToString()),
                //new Claim(ClaimsIdentity.DefaultRoleClaimType,"Admin"),
                new Claim("Email", user.Profile.Email),
                new Claim("Type", user.UserType.ToString())
            });
            var roles = await this.UserManager.GetRolesAsync(user);
            foreach (var role in roles) {
                so.Add(new Claim(ClaimsIdentity.DefaultRoleClaimType, role));
            }
            //var roles = Enumerable.Range(1, 4).Select(x => new Claim($"role", $"qwe{x}")).ToArray();
            ClaimsIdentity identity = new ClaimsIdentity(
                new GenericIdentity(user.UserName, "TokenAuth"),
                so
            );
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor {
                Issuer = TokenAuthOption.Issuer,
                Audience = TokenAuthOption.Audience,
                SigningCredentials = TokenAuthOption.SigningCredentials,
                Subject = identity,
                Expires = expires,
            });
            return handler.WriteToken(securityToken);
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "KindergartenListEdit")]
        public IActionResult ForAdmin()
        {
            return this.Json("Yes is Admin");
        }
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult ForAuth()
        {
            return this.Json("Yes is auth");
        }
        public IActionResult ForAll()
        {
            return this.Json("Yes is All");
        }
    }
}
