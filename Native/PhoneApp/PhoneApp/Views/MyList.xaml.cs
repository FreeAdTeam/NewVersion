using System;
using System.Collections.Generic;
using PhoneApp.Models;
using Xamarin.Forms;

namespace PhoneApp
{
	public partial class MyList : CarouselPage
	{
		MyListViewModel vm;
		public MyList ()
		{
			Title = "My Watch List";
			vm = new MyListViewModel ();
			BindingContext = vm;
			InitializeComponent ();
		}
		public void OnItemTapped(object o, ItemTappedEventArgs e)
		{
			var ad = e.Item as Advertisement;
			var detail = new AdvertisementDetail(ad);
			Navigation.PushAsync(detail);
		}
	}
}

