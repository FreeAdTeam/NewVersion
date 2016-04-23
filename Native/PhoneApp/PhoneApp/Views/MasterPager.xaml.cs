using System;
using System.Collections.Generic;
using Xamarin.Forms;
namespace PhoneApp
{
	public partial class MasterPage : ContentPage
	{
		public ListView ListView { get { return listView; } }

		public MasterPage ()
		{
			InitializeComponent ();
			//BackgroundImage = "ground.png";
			var masterPageItems = new List<MasterPageItem> ();
			masterPageItems.Add (new MasterPageItem {
				Title = "Main page",
				IconSource = "main.png",
				TargetType = typeof(Home)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "My watch list",
				IconSource = "list.png",
				TargetType = typeof(MyList)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Settings",
				IconSource = "settings.png",
				TargetType = typeof(MySettings)
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Manual",
				IconSource = "manual.png",
				TargetType = typeof(Others),
				Parameter="http://ouroptions.co.nz/PoneAdvertisement/manual"
			});
			masterPageItems.Add (new MasterPageItem {
				Title = "Contact us",
				IconSource = "info.png",
				TargetType = typeof(Others),
				Parameter="http://ouroptions.co.nz/PoneAdvertisement/contact"
			});
			listView.ItemsSource = masterPageItems;
		}
	}
}

