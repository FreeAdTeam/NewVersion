using Newtonsoft.Json;
using PhoneApp.Helpers;
using PhoneApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.ComponentModel;
using Xamarin.Forms;

namespace PhoneApp.ViewModels
{
	public class HomeViewModel:INotifyPropertyChanged
    {
		public HomeViewModel ()
		{
			this.Connect = new Command (() => {
				Ads ();
			});
			this.Search = new Command (() => {
				var search = string.IsNullOrEmpty (searchText) ? "none" : searchText;
				Ads (categorySelectedIndex, search);
			});
			this.SearchChanged = new Command (() => {
				if (string.IsNullOrEmpty (searchText)) {
					Ads (categorySelectedIndex);
				}
			});
			Ads ();
		}
		public event PropertyChangedEventHandler PropertyChanged;
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
		private List<Advertisement> data;

		public List<Advertisement> Data{ get{ return data; } set{
				if (data != value)
				{
					data = value;
					OnPropertyChanged("Data");
				} 
			} 
		}
		private bool connectShow;
		public bool ConnectShow{ get{ return connectShow; } set{
				if (connectShow != value)
				{
					connectShow = value;
					OnPropertyChanged("ConnectShow");
				} 
			} 
		}
		private string searchText;
		public string SearchText{ get{ return searchText; } set{
				if (searchText != value)
				{
					searchText = value;
					OnPropertyChanged("SearchText");
				} 
			} 
		}

		private int categorySelectedIndex;
		public int CategorySelectedIndex{ get{ return categorySelectedIndex; } set{
				if (categorySelectedIndex != value)
				{
					categorySelectedIndex = value;
					OnPropertyChanged("CategorySelectedIndex");
				} 
			} 
		}
		public ICommand Connect { protected set; get; }
		public ICommand Search { protected set; get; }
		public ICommand SearchChanged { protected set; get; }

		public async void Ads(int categoryIndex=-1,string search="none")
        {
			var categoryId = 0;
			switch (categoryIndex) {
			case -1: //all
				categoryId = 0;
				break;
			case 1:// "Companies":
				categoryId = 2;
				break;
			case 2://"Recruits":
				categoryId = 4;
				break;
			case 3://"RentRooms":
				categoryId = 6;
				break;
			case 4://"Products":
				categoryId = 7;
				break;
			case 5://"PersonalInfo":
				categoryId = 18;
				break;
			default:
				categoryId = 0;
				break;
			}
			try {
				var client = ADHttpClient.GetClient();
				var egsResponse = await client.GetAsync("api/category/"+categoryId.ToString()+"/advertisements?search="+search+"&fields=id,keyword,shortdescription,categoryid");
				if (egsResponse.IsSuccessStatusCode)
				{
					string egsContent = await egsResponse.Content.ReadAsStringAsync();
					var lst = JsonConvert.DeserializeObject<List<Advertisement>>(egsContent);
					Data=lst;
					ConnectShow = false;
				}
				else
				{
					ConnectShow = true;
				}
			} catch (Exception) {
				ConnectShow = true;
			}
        }
    }
}
