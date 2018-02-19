using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using DAL.Entities.Account;
using DAL.Repositories;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using IdentityResult = Microsoft.AspNetCore.Identity.IdentityResult;

namespace WebApi.Auth
{
    public class ApplicationSignInManger<TUser> : Microsoft.AspNetCore.Identity.SignInManager<User> where TUser : class{
        private readonly Repository<User> _repository;
        public Repository<User> GetCurrentRepository => this._repository;
        public ApplicationSignInManger(Microsoft.AspNetCore.Identity.UserManager<User> userManager,
            IHttpContextAccessor contextAccessor, IUserClaimsPrincipalFactory<User> claimsFactory,
            IOptions<IdentityOptions> optionsAccessor, ILogger<SignInManager<User>> logger,
            IAuthenticationSchemeProvider schemes) : base(userManager, contextAccessor, claimsFactory, optionsAccessor,
            logger, schemes) {
        }
    }
    public class ApplicationUserManager<T>:Microsoft.AspNetCore.Identity.UserManager<DAL.Entities.Account.User>
    {
        public ApplicationUserManager(Microsoft.AspNetCore.Identity.IUserStore<User> store,
            IOptions<IdentityOptions> optionsAccessor, IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators, IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer, IdentityErrorDescriber errors, IServiceProvider services,
            ILogger<Microsoft.AspNetCore.Identity.UserManager<User>> logger) : base(store, optionsAccessor,
            passwordHasher, userValidators, passwordValidators, keyNormalizer, errors, services, logger) { }
    }
    public class ApplicationUserStore : Microsoft.AspNetCore.Identity.IUserStore<User>, Microsoft.AspNetCore.Identity.IUserPasswordStore<User>
    {

        private readonly Repository<User> _repository;
        public Repository<User> GetCurrentRepository => this._repository;
        public ApplicationUserStore(SignInRepository rep) {
            this._repository = rep.Repository;
        }
        public void Dispose()
        {
            this._repository.Dispose();
        }
        public ApplicationUserStore(Repository<User> repository)
        {
            this._repository = repository;
        }
        public Task<string> GetUserIdAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Id.ToString());
        }

        public Task<string> GetUserNameAsync(User user, CancellationToken cancellationToken) {
           return  Task.FromResult(user.UserName);
        }

        public Task SetUserNameAsync(User user, string userName, CancellationToken cancellationToken) {
            return Task.FromResult(0);
        }

        public Task<string> GetNormalizedUserNameAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.UserName);
        }

        public Task SetNormalizedUserNameAsync(User user, string normalizedName, CancellationToken cancellationToken) {
            return Task.FromResult(0);
        }

        public async Task<IdentityResult> CreateAsync(User user, CancellationToken cancellationToken) {
            return await Task.Run(() => {
                this._repository.Insert(user);
                this._repository.Commit();
                return new IdentityResult();
            });
        }

        public Task<IdentityResult> UpdateAsync(User user, CancellationToken cancellationToken) {
            return Task.Run(() => {
                this._repository.Update(user);
                this._repository.Commit();
                return new IdentityResult();
            });
        }

        public Task<IdentityResult> DeleteAsync(User user, CancellationToken cancellationToken) {
            return Task.Run(() => {
                this._repository.Delete(user);
                this._repository.Commit();
                return new IdentityResult();
            });
        }

        public Task<User> FindByIdAsync(string userId, CancellationToken cancellationToken) {
            var id = Convert.ToInt32(userId);
            return Task.Run(() => this._repository.Get(x => x.Id == id).SingleOrDefault());
        }

        public Task<User> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken) {
            return Task.Run(() => this._repository.Get(x => x.UserName == normalizedUserName).SingleOrDefault());
        }

        public Task SetPasswordHashAsync(User user, string passwordHash, CancellationToken cancellationToken) {
            user.Password = passwordHash;
            user.LastPasswordChangedDate = DateTime.Now;
            return Task.FromResult(0);
        }

        public Task<string> GetPasswordHashAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(user.Password);
        }

        public Task<bool> HasPasswordAsync(User user, CancellationToken cancellationToken) {
            return Task.FromResult(!string.IsNullOrEmpty(user.Password));
        }
    }

    public interface IUserManger {

    }
}
