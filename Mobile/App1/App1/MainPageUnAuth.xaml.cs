using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Extenssions;
using App1.Models;
using App1.Notifiers;
using App1.Services;
using App1.ViewModels;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace App1
{
	public partial class MainPageUnAuth : ContentPage {
	    private AuthenticationService _authService;
	    private AuthViewModel _authViewModel;
        public MainPageUnAuth() {

            InitializeComponent();
            this._authService=new AuthenticationService();
            var notifier = new DisplayAlertNotifier(this);
            this.BindingContext = this._authViewModel = new AuthViewModel(this.Navigation, notifier);
            var semiTransparentColor = new Color(255, 255, 255, 0.14);
            this.btnSignIn.BackgroundColor = semiTransparentColor;
            this.btnSignIn.BorderColor = semiTransparentColor;
        }
	}
}
