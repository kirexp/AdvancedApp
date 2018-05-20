using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Notifiers;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using RentViewModel = App1.Services.RentViewModel;

namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RentCreationPage : ContentPage {
	    private RentCreationViewModel _rentViewModel;
        public RentCreationPage ()
		{
			InitializeComponent ();
		    var alertNotifier = new DisplayAlertNotifier(this);
            BindingContext=this._rentViewModel=new RentCreationViewModel(Navigation, alertNotifier);
		    _rentViewModel.ShowRentInfo.PropertyChanged+=ShowRentInfoOnPropertyChanged;
		    this.dataFreme.IsVisible = false;
		    //Task.Run(async () => { await dataFreme.FadeTo(0, 1); });
		}
        private async void ShowRentInfoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            if (!this._rentViewModel.ShowRentInfo.Value) {
                await dataFreme.FadeTo(0, 600);
	            this.dataFreme.IsVisible = this._rentViewModel.ShowRentInfo.Value;

	        }
	        else{
                this.dataFreme.IsVisible = this._rentViewModel.ShowRentInfo.Value;
                await dataFreme.FadeTo(1, 600);

            }
        }
	}
}