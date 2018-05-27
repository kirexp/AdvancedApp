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
        public bool HasRent { get; set; }
        public bool HasntRent => !this.HasRent;
        public VehicleDto VehicleDto { get; set; }
        public RentCreationViewModel(INavigation navigation, INotifier notifier) : base(navigation, notifier) {
            Zoos = new ObservableCollection<Zoo>
            {
                new Zoo
                {
                    ImageUrl = "http://www.shinycars.cl/wp-content/uploads/2015/04/mercedes-benz-c250-2-200x300.jpg",
                    Name = "Photo1"
                },
                new Zoo
                {
                    ImageUrl =    "http://www.shinycars.cl/wp-content/uploads/2015/04/mercedes-benz-cla-250-27-300x200.jpg",
                    Name = "Cleveland Zoo"
                },
                new Zoo
                {
                    ImageUrl = "http://www.shinycars.cl/wp-content/uploads/2015/04/mercedes-benz-ml-350-5-300x225.jpg",
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
