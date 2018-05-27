using System.Threading.Tasks;
using Android.Content;
using App1.Droid.Services;
using App1.Services;
using Dependency = Xamarin.Forms.DependencyAttribute;

[assembly: Dependency(typeof(AndroidPhone))]
namespace App1.Droid.Services {
    public class AndroidPhone : IPhone {
        public Task Call(string phoneNumber) {
            var packageManager = Android.App.Application.Context.PackageManager;
            Android.Net.Uri telUri = Android.Net.Uri.Parse($"tel:{phoneNumber}");
            var callIntent = new Intent(Intent.ActionCall, telUri);

            callIntent.AddFlags(ActivityFlags.NewTask);
            // проверяем доступность
            var result = null != callIntent.ResolveActivity(packageManager);

            if (!string.IsNullOrWhiteSpace(phoneNumber) && result == true) {
                Android.App.Application.Context.StartActivity(callIntent);
            }

            return Task.FromResult(true);
        }
    }
}