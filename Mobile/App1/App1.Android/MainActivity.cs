using System;
using System.Linq;
using Android.App;
using Android.Content.PM;
using Android.Locations;
using Android.OS;

namespace App1.Droid {
    [Activity(Label = "App1", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ILocationListener {

        private LocationManager _locationManager;
        public static Location LastKnownLocation;
        private string _locationProvider;

        protected override void OnCreate(Bundle bundle) {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());
            InitializeLocationManager();
        }

        public void OnLocationChanged(Location location) {
            LastKnownLocation = location;
        }

        public void OnProviderDisabled(string provider) { }

        public void OnProviderEnabled(string provider) { }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) { }

        protected override void OnResume() {
            base.OnResume();
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }

        protected override void OnPause() {
            base.OnPause();
            _locationManager.RemoveUpdates(this);
        }

        private void InitializeLocationManager() {
            _locationManager = (LocationManager)GetSystemService(LocationService);
            var criteriaForLocationService = new Criteria {
                Accuracy = Accuracy.Fine
            };
            var acceptableLocationProviders = _locationManager.GetProviders(criteriaForLocationService, true);

            _locationProvider = acceptableLocationProviders.Any()
                ? acceptableLocationProviders.First()
                : throw new Exception("Нет доступных провайдеров");
            _locationManager.RequestLocationUpdates(_locationProvider, 0, 0, this);
        }
    }
}

