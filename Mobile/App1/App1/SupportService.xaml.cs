using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class SupportService : ContentPage
	{
		public SupportService ()
		{
			InitializeComponent ();
		}

	    private async void Call(object sender, EventArgs e) {
	        var service = DependencyService.Get<IPhone>();
	        if (service != null) {
	            await service.Call("+77759212141");
	            return;
	        }
	        await DisplayAlert("Error", "Все пошло по пизде", "Ok");
        }
	}
}