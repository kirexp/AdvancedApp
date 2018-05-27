using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.ApiDTO;
using App1.Extenssions;
using App1.Helpers;
using App1.Notifiers;
using App1.Services;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Cabinet : ContentPage {
        private readonly RentCreationViewModel _rentViewModel;
        private Http _http;

        public Cabinet() {
            this._http = new Http();
            InitializeComponent();
            var alertNotifier = new DisplayAlertNotifier(this);
            if (SettingsManager.Instance.HasRent) {
                var task = Task.Run(async () => await Init());
                task.ContinueWith(vTask => {
                    BindingContext = new RentCreationViewModel(Navigation, alertNotifier) { VehicleDto = vTask.Result,HasRent = true};
                }, TaskContinuationOptions.NotOnFaulted);
                task.Wait();
            } else {
                BindingContext = this._rentViewModel = new RentCreationViewModel(Navigation, alertNotifier) { HasRent = false };
            }
        }

        private async Task<VehicleDto> Init() {
            var rentId = SettingsManager.Instance.RentId;
           var result = await this._http.GetAsync<SimpleResponse<VehicleDto>>("Reserve/GetRent?id=" + rentId);
            return result.Data;
        }

        private async void CancelRent(object sender, EventArgs e) {
            var rentId = SettingsManager.Instance.RentId;

            var lp = new LocationProvider();
            var position = await lp.GetGps();
            var coord = new Coordinates {
                Latitude = (decimal)position.Latitude,
                Longitude = (decimal)position.Longitude
            };

            var undoRequest = new RentUndoRequest {
                RendId = rentId,
                CurrentPosition = coord
            };
            var result = await this._http.PostAsJson<SimpleResponse>("/Reserve/CancelRent", undoRequest);
            if (result.IsSuccess) {
                SettingsManager.Instance.HasRent = false;
                SettingsManager.Instance.RentId = 0;
            }
        }

        private async void FinishRent(object sender, EventArgs e) {
            var rentId = SettingsManager.Instance.RentId;

            var lp = new LocationProvider();
            var position = await lp.GetGps();
            var coord = new Coordinates {
                Latitude = (decimal)position.Latitude,
                Longitude = (decimal)position.Longitude
            };

            var finisthRequest = new FinishRentRequest {
                RentId = rentId,
                CurrentPosition = coord
            };
            var result = await this._http.PostAsJson<SimpleResponse>("/Reserve/CancelRent", finisthRequest);
            if (result.IsSuccess) {
                SettingsManager.Instance.HasRent = false;
                SettingsManager.Instance.RentId = 0;
            }
        }
    }
    public class RentUndoRequest {
        public long RendId { get; set; }
        public Coordinates CurrentPosition { get; set; }

    }
    public class FinishRentRequest {
        public long RentId { get; set; }
        public Coordinates CurrentPosition { get; set; }

    }
}