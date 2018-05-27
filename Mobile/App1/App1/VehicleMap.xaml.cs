using System;
using System.Collections.ObjectModel;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using App1.ApiDTO;
using App1.Extenssions;
using App1.Services;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class VehicleMap : TabbedPage {
        public ObservableCollection<VehicleDto> _vehicles;
        private readonly RentService _rentService;
        private DateTime? _lastLoadTime;
        public VehicleMap() {
            _vehicles = new ObservableCollection<VehicleDto>();
            _rentService = new RentService();
            InitializeComponent();
            //this.viewBrowser.Source = new UrlWebViewSource {Url = "/html/HTMLPage1.html"};
            this.viewBrowser.Navigating += ViewBrowserOnNavigating;
            this.carList.ItemsSource = this._vehicles;
        }
        private async void ViewBrowserOnNavigating(object sender, WebNavigatingEventArgs webNavigatingEventArgs) {
            var service = new RentService();
            var regex = Regex.Match(webNavigatingEventArgs.Url, "(carId=[\\d]{1,999})");
            var id = int.Parse(regex.Value.Replace("carId=", ""));
            var vehicleDto = await service.GetVehicle(id);
            await this.Navigation.PushAsync(new RentCreationPage(vehicleDto));
            // webNavigatingEventArgs.Cancel = true;
            //this.Navigation.PushAsync()
        }
        private async void CarList_OnItemTapped(object sender, ItemTappedEventArgs e) {
            if (e.Item is VehicleDto vdto) {
                var page = new RentCreationPage(vdto);
                await Navigation.PushAsync(page);
            }
        }

        private async void OnTabChanged(object sender, EventArgs e) {
            if (this._lastLoadTime == null) {
                this._lastLoadTime = DateTime.Now;
            } else {
                if (DateTime.Now.Subtract(this._lastLoadTime.Value) > TimeSpan.FromSeconds(5)) {
                    if (SettingsManager.Instance.IsAuthentithicated && SettingsManager.Instance.Expires > DateTime.Now) {
                        this.carList.ItemsSource = this._vehicles =
                            new ObservableCollection<VehicleDto>(await this._rentService.GetUnReservedVehicles());
                        this._lastLoadTime = DateTime.Now;
                    } else {
                        App.SetMainPage();
                    }
                }
            }
        }
    }
}