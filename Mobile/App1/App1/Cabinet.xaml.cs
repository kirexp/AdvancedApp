using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.ApiDTO;
using App1.Extenssions;
using App1.Notifiers;
using App1.Services;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Cabinet : ContentPage
	{
	    private readonly RentCreationViewModel _rentViewModel;
	    private Http _http;
        
        public Cabinet () {
            this._http = new Http();
			InitializeComponent ();
		    var alertNotifier = new DisplayAlertNotifier(this);

		    BindingContext = this._rentViewModel = new RentCreationViewModel(Navigation, alertNotifier) { VehicleDto = Init().GetAwaiter().GetResult() };

        }

        private async Task<VehicleDto> Init() {
            var rentId = SettingsManager.Instance.RentId;
           var result = await this._http.GetAsync<VehicleDto>("/Reserve/GetRent?Id=" + rentId);
            return result;
        }
	}
}