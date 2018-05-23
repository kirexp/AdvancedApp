using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using App1.ApiDTO;
using App1.Models;
using App1.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class VehicleMap : TabbedPage {
	    public ObservableCollection<VehicleDto> _vehicles;
	    private RentService _rentService;
	    private DateTime? _lastLoadTime;
		public VehicleMap()
		{
		    _vehicles = new ObservableCollection<VehicleDto>();
		    _rentService=new RentService();
            InitializeComponent();
            this.viewBrowser.Navigating += ViewBrowserOnNavigating;
            this.viewBrowser.Navigated += webOnEndNavigating;
            this.carList.ItemsSource = this._vehicles;
        }
	    void webOnEndNavigating(object sender, WebNavigatedEventArgs e) {
	        this.DisplayAlert("end", "end", "s");
	    }
        private async void ViewBrowserOnNavigating(object sender, WebNavigatingEventArgs webNavigatingEventArgs) {
            var service = new RentService();
           //var result= await service.CreateRentAsync(1);
	        var z = 0;
            // webNavigatingEventArgs.Cancel = true;
            this.Navigation.PushAsync()
        }
	    private async Task CarList_OnItemTapped(object sender, ItemTappedEventArgs e) {
	        var rentService = new RentService();
	        var result = await rentService.CreateRentAsync((e.Item as  VehicleDto).Id);
	        var z = 0;
	    }

	    private async void OnTabChanged(object sender, EventArgs e) {
	        if (this._lastLoadTime == null) {
	            this._lastLoadTime = DateTime.Now;
	        }
	        else {
	            if (DateTime.Now.Subtract(this._lastLoadTime.Value) > TimeSpan.FromSeconds(5)) {
	                this.carList.ItemsSource = this._vehicles =
	                    new ObservableCollection<VehicleDto>(await this._rentService.GetUnReservedVehicles());
	                this._lastLoadTime = DateTime.Now;
	            }
	        }
	    }
	}
}