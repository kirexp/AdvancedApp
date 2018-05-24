using System;
using App1.Extenssions;
using Xamarin.Forms;

namespace App1 {
    public partial class App : Application {
        public App() {
            InitializeComponent();
            SetMainPage();

        }

        public static void SetMainPage() {
            if (SettingsManager.Instance.IsAuthentithicated && SettingsManager.Instance.Expires > DateTime.Now)
                Current.MainPage = new MainPageAuth();
            else
                Current.MainPage = new MainPageUnAuth();
        }
        protected override void OnStart() {
            // Handle when your app starts
        }

        protected override void OnSleep() {
            // Handle when your app sleeps
        }

        protected override void OnResume() {
            SetMainPage();
        }
    }
}
