using System;
using System.Collections.Generic;
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
                var payLoad = result.Token.accessToken.Split('.')[1];
                var z1 = Convert.FromBase64String(payLoad);
                var z2 = Encoding.UTF8.GetString(z1);
                var data = JsonConvert.DeserializeObject<UserDto>(z2);
                SettingsManager.Instance.UserName = data.UserName;
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
