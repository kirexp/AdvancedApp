using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Windows.Input;
using App1.ApiDTO;
using App1.Validation;
using Xamarin.Forms;

namespace App1.ViewModels {
    public class RentCreationViewModel : BaseViewModel {
        public ObservableCollection<Zoo> Zoos { get; set; }
        private ValidatableObject<bool> _showRentInfo;
        public ValidatableObject<bool> ShowRentInfo {
            get { return _showRentInfo; }
            set { SetProperty(ref _showRentInfo, value); }
        }
        public VehicleDto VehicleDto { get; set; }
        public RentCreationViewModel(INavigation navigation, INotifier notifier) : base(navigation, notifier) {
            Zoos = new ObservableCollection<Zoo>
            {
                new Zoo
                {
                    ImageUrl = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/23c1dd13-333a-459e-9e23-c3784e7cb434/2016-06-02_1049.png",
                    Name = "Woodland Park Zoo"
                },
                new Zoo
                {
                    ImageUrl =    "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/6b60d27e-c1ec-4fe6-bebe-7386d545bb62/2016-06-02_1051.png",
                    Name = "Cleveland Zoo"
                },
                new Zoo
                {
                    ImageUrl = "http://content.screencast.com/users/JamesMontemagno/folders/Jing/media/e8179889-8189-4acb-bac5-812611199a03/2016-06-02_1053.png",
                    Name = "Phoenix Zoo"
                }
            };
            this._showRentInfo = new ValidatableObject<bool>(false);
        }
    }
    public class Zoo {
        public string ImageUrl { get; set; }
        public string Name { get; set; }
    }
}
