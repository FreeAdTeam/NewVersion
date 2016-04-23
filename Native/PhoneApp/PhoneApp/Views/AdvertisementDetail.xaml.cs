using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhoneApp.Models;
using Xamarin.Forms;

namespace PhoneApp
{
    public partial class AdvertisementDetail : ContentPage
    {
		AdvertisementDetailViewModel vm;
		public AdvertisementDetail(Advertisement ad)
		{
			vm = new AdvertisementDetailViewModel (ad);
			this.Title = "Detail Information";
			BindingContext = vm;
			InitializeComponent ();
			ConnectButton.Clicked += OnConnectClick;
		}
		void OnConnectClick(object sender, EventArgs e){
			Device.BeginInvokeOnMainThread(() =>
				{
					vm = new AdvertisementDetailViewModel (vm.Ad);
					this.Title = "Detail Information";
					BindingContext = vm;
					InitializeComponent ();
					ConnectButton.Clicked += OnConnectClick;
				});
		}
        void webviewNavigating(object sender, WebNavigatingEventArgs e)
        {
			vm.WebViewShow = false;
			vm.AddShow = false;
			vm.IndicatorShow = true;
        }
        void webviewNavigated(object sender, WebNavigatedEventArgs e)
        {
			if (e.Result.ToString()=="Success") {
				vm.AddShow = true;
				vm.WebViewShow = true;
				vm.IndicatorShow = false;
				vm.ConnectShow = false;
			} else {
				vm.AddShow = false;
				vm.WebViewShow = false;
				vm.IndicatorShow = false;
				vm.ConnectShow = true;
			}
        }

    }
}
