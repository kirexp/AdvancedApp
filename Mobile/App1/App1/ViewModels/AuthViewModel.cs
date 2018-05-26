using System;
using System.Collections.Generic;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Xml.Schema;
using App1.Extenssions;
using App1.Helpers;
using App1.Models;
using App1.Services;
using App1.Validation;
using App1.Validation.Rules;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace App1.ViewModels
{
    public class AuthViewModel: BaseViewModel {
        private AuthenticationService _authService;
        private ValidatableObject<string> _userName;
        private ValidatableObject<string> _password;
        public ICommand ValidateUserNameCommand => new Command(() => ValidateUserName());
        public ICommand SignInCommand => new Command( async() => await SignIn());
        public ValidatableObject<string> UserName {
            get { return this._userName; }
            set {
                SetProperty(ref this._userName, value);
            }
        }
        private bool ValidateUserName() {
            return this._userName.Validate();
        }
        private async Task SignIn() {
            try
            {
                var result = await _authService.Authenticate(UserName.Value, Password.Value);
                if (!result.IsSuccess)
                {
                    this.Alert("Внимание", result.ErrorText);
                    return;
                }
                SettingsManager.Instance.AuthToken = result.Token.accessToken;
                SettingsManager.Instance.IsAuthentithicated = true;
                
                var handler = new JwtSecurityTokenHandler();
                var tokenS = handler.ReadToken(result.Token.accessToken) as JwtSecurityToken;

                var exp1 = tokenS.Claims.First(claim => claim.Type == "exp").Value;
                var ticks = long.Parse(exp1);
                //var ts = new TimeSpan(ticks);

                var expires = DateTime.Now.AddTicks(ticks);

                //var exp2 = tokenS.Claims.First(claim => claim.Type == "ExpirationDateTime").Value;
                //var date = DateTime.ParseExact(exp2, "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

                var data = new UserDto {
                    Email = tokenS.Claims.First(claim => claim.Type == "Email").Value,
                    Id = long.Parse(tokenS.Claims.First(claim => claim.Type == "Id").Value),
                    Type = tokenS.Claims.First(claim => claim.Type == "Type").Value,
                    UserName = tokenS.Claims.First(claim => claim.Type == "unique_name").Value,
                };

                SettingsManager.Instance.UserName = data.UserName;
                SettingsManager.Instance.Expires = expires;
                App.SetMainPage();
            }
            catch (Exception ex)
            {
                SettingsManager.Instance.IsAuthentithicated = false;
                this.Alert("Внимание","Ошибка");
            }
        }
        public AuthViewModel(INavigation navigation, INotifier notifier) : base(navigation,notifier) {
            this._userName=new ValidatableObject<string>();
            this._password = new ValidatableObject<string>();
            this._authService=new AuthenticationService();
            AddValidator();
        }
        private void AddValidator() {
            this._userName.Validations.Add(new PatternRule("([A-Za-z0-9])", "Логин должен состоять из символом или цифр"));
        }
        public ValidatableObject<string> Password
        {
            get { return this._password; }
            set {
                SetProperty(ref this._password, value);
            }
        }
    }
}
