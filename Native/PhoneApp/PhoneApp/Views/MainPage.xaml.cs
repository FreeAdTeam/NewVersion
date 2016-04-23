using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using PhoneApp;

namespace PhoneApp
{
    public partial class MainPage : MasterDetailPage
    {
        public MainPage()
        {
            InitializeComponent();
            masterPage.ListView.ItemSelected += OnItemSelected;
            if (Device.OS == TargetPlatform.Windows)
            {
                Master.Icon = "swap.png";
            }

        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var item = e.SelectedItem as MasterPageItem;
            if (item != null)
            {
				Detail = item.Parameter==null?new NavigationPage((Page)Activator.CreateInstance(item.TargetType)):new NavigationPage((Page)Activator.CreateInstance(item.TargetType,item.Parameter));
                //masterPage.ListView.SelectedItem = null;
                IsPresented = false;
            }
        }
    }
}
