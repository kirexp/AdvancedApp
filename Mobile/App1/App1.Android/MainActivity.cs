using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Locations;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace App1.Droid
{
    [Activity(Label = "App1", Icon = "@drawable/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity, ILocationListener
    {
        protected override void OnCreate(Bundle bundle)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            LoadApplication(new App());

            LocationManager locationManager = (LocationManager)GetSystemService(Context.LocationService);
        }

        public void OnLocationChanged(Location location) {
            throw new NotImplementedException();
        }

        public void OnProviderDisabled(string provider) {
            throw new NotImplementedException();
        }

        public void OnProviderEnabled(string provider) {
            throw new NotImplementedException();
        }

        public void OnStatusChanged(string provider, Availability status, Bundle extras) {
            throw new NotImplementedException();
        }
    }
}

