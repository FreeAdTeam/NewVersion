using System;
using System.Collections.Generic;
using PhoneApp.ViewModels;
using Xamarin.Forms;

namespace PhoneApp
{
	public partial class Others : ContentPage
	{
		OthersViewModel vm;
		string para;
		public Others (string parameter)
		{
			para = parameter;
			vm = new OthersViewModel ();
			vm.URL = parameter;
			Title=parameter.Contains("manual")?"Manual": "Contact US";
			BindingContext = vm;
			InitializeComponent ();
			ConnectButton.Clicked += OnConnectClick;
		}
		void OnConnectClick(object sender, EventArgs e){
			Device.BeginInvokeOnMainThread(() =>
				{
					vm = new OthersViewModel ();
					vm.URL=para;
					Title=para.Contains("manual")?"Manual": "Contact US";
					BindingContext = vm;
					InitializeComponent ();
					ConnectButton.Clicked += OnConnectClick;
				});
		}
		void webviewNavigating(object sender, WebNavigatingEventArgs e)
		{
			vm.WebViewShow = false;
			vm.IndicatorShow = true;
			vm.ConnectShow = false;
		}
		void webviewNavigated(object sender, WebNavigatedEventArgs e)
		{
			if (e.Result.ToString () == "Success") {
				vm.WebViewShow = true;
				vm.IndicatorShow = false;
				vm.ConnectShow = false;
			} else {
				vm.WebViewShow = false;
				vm.IndicatorShow = false;
				vm.ConnectShow = true;
			}
		}
	}
}

