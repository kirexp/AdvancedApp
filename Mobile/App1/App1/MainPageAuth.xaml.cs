using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App1.Extenssions;
using App1.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App1
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPageAuth : MasterDetailPage
	{
        private UserViewModel _ViewModel { get; set; }
		public MainPageAuth()
		{
			InitializeComponent ();

            Detail = new NavigationPage(new History());
		    if (SettingsManager.Instance.IsAuthentithicated) {
		        this._ViewModel =new UserViewModel();
		        this.BindingContext = _ViewModel;
            }
		}

	    private void HistoryRedirectBtn_Clicked(object sender, EventArgs e) {
	        this.Detail= new NavigationPage(new History());
	        this.HideDetail();
        }

	    private void RentACarRedirectBtn_Clicked(object sender, EventArgs e) {
	        this.Detail = new NavigationPage(new VehicleMap());
	        this.HideDetail();
        }

	    private void AboutPageBtn_Clicked(object sender, EventArgs e) {
	     this.Detail=new NavigationPage(new AboutPage());
	        this.HideDetail();
	    }

	    private void HideDetail() {
	        this.IsPresented = false;
	    }

	    private void SignOutBtn_Clicked(object sender, EventArgs e) {
	        SettingsManager.Instance.IsAuthentithicated = false;
	       App.SetMainPage();
	    }

	    private void CabinetBtn_Clicked(object sender, EventArgs e) {
            this.Detail = new NavigationPage(new Cabinet());
            this.HideDetail();
        }

	    private void FaqBtn_Clicked(object sender, EventArgs e) {
	        this.Detail = new NavigationPage(new SupportService());
	        this.HideDetail();
        }
	}
}