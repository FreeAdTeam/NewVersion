using PhoneApp.Models;
using PhoneApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PhoneApp
{
    public partial class Home : ContentPage
    {
        HomeViewModel vm;
		public Home()
        {
            vm = new HomeViewModel();
			BindingContext = vm;
            InitializeComponent();
			Title="Advertisement platform";
			CategoryPicker.Title = "Category Setting Default All";
			CategoryPicker.Items.Add("All");
			CategoryPicker.Items.Add("Companies");
			CategoryPicker.Items.Add("Recruits");
			CategoryPicker.Items.Add("RentRooms");
			CategoryPicker.Items.Add("Products");
			CategoryPicker.Items.Add("PersonalInfo");

			CategoryPicker.SelectedIndexChanged += (sender, args) =>
			{
				var search=string.IsNullOrEmpty(vm.SearchText)?"none":vm.SearchText.Trim();
				var categoryIndex=CategoryPicker.SelectedIndex;
				vm.Ads(categoryIndex,search);
			};
			SearchBar.TextChanged += MySearchBarOnTextChanged;
        }

		private void MySearchBarOnTextChanged(object sender, TextChangedEventArgs textChangedEventArgs)
		{
			if (string.IsNullOrEmpty(textChangedEventArgs.NewTextValue ))
			{
				vm.Ads (CategoryPicker.SelectedIndex);
			}
		}

        public void OnItemTapped(object o, ItemTappedEventArgs e)
        {
            var ad = e.Item as Advertisement;
            var detail = new AdvertisementDetail(ad);
            Navigation.PushAsync(detail);
        }
    }
}
