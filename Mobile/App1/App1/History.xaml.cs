using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using App1.Extenssions;
using App1.Services;
using Newtonsoft.Json;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1 {
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class History : ContentPage {
        private RentViewModel _rent;
        private SummaryViewModel _summary;
        private CabinetService _cabinetService;
        public History() {
            InitializeComponent();
            this._cabinetService = new CabinetService();
            LoadOwnData();
        }

        private async void LoadOwnData() {
            if (SettingsManager.Instance.IsAuthentithicated && SettingsManager.Instance.Expires > DateTime.Now) {
                this._rent = await _cabinetService.GetRent();
                this._summary = await _cabinetService.GetSummary();
            } else {
                App.SetMainPage();
            }
        }
        private void VisualElement_OnFocused(object sender, FocusEventArgs e) {
            this.allSummaryFrame.BackgroundColor = Color.AliceBlue;
        }

        private void LastRentFrame_OnFocused(object sender, FocusEventArgs e) {
            this.lastRentFrame.BackgroundColor = Color.AliceBlue;
        }
    }
}