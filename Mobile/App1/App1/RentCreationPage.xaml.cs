using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using App1.ApiDTO;
using App1.Extenssions;
using App1.Notifiers;
using App1.Services;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RentCreationPage : ContentPage {
        private readonly RentCreationViewModel _rentViewModel;

        public RentCreationPage(VehicleDto vdto) {
            InitializeComponent();

            var alertNotifier = new DisplayAlertNotifier(this);
            BindingContext = this._rentViewModel = new RentCreationViewModel(Navigation, alertNotifier) { VehicleDto = vdto };
            _rentViewModel.ShowRentInfo.PropertyChanged += ShowRentInfoOnPropertyChanged;
            this.dataFreme.IsVisible = false;
            //Task.Run(async () => { await dataFreme.FadeTo(0, 1); });
        }
        private async void ShowRentInfoOnPropertyChanged(object sender, PropertyChangedEventArgs propertyChangedEventArgs) {
            if (!this._rentViewModel.ShowRentInfo.Value) {
                await dataFreme.FadeTo(0, 600);
                this.dataFreme.IsVisible = this._rentViewModel.ShowRentInfo.Value;

            } else {
                this.dataFreme.IsVisible = this._rentViewModel.ShowRentInfo.Value;
                await dataFreme.FadeTo(1, 600);

            }
        }

        private async void Create(object sender, EventArgs e) {
            if (SettingsManager.Instance.IsAuthentithicated && SettingsManager.Instance.Expires > DateTime.Now) {
                var model = BindingContext as RentCreationViewModel;
                var rs = new RentService();
                var result = await rs.CreateRentAsync(model);
                if (result.IsSuccess) {
                    SettingsManager.Instance.RentId = result.Data;
                    SettingsManager.Instance.HasRent = true;
                }
                else {
                    await this.DisplayAlert("Внимание", $"Не удалось сделать аренду в связи с - {result.ErrorText}", "ok");
                }

            } else {
                App.SetMainPage();
            }
        }
    }
}