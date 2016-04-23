using System;
using System.Windows.Input;
using Xamarin.Forms;
using PhoneApp.Models;
using System.ComponentModel;

namespace PhoneApp
{
	public class AdvertisementDetailViewModel:INotifyPropertyChanged
	{
		LocalData _db=new LocalData();
		public event PropertyChangedEventHandler PropertyChanged;
		public Advertisement Ad { get; set;}

		public ICommand AddCommand { protected set; get; }
		public AdvertisementDetailViewModel (Advertisement ad)
		{
			URL = "http://ouroptions.co.nz/PoneAdvertisement/Index/" + ad.Id;
			Ad=ad;
			this.AddCommand = new Command(() =>
				{
					if(inLocalDB(Ad.Id)){
						ButtonText=_db.DeleteAd(Ad.Id)>0?"Bring It In":"Delete It From My List";
					}
					else
					{
						var saveReturn=_db.SaveAd (new Advertisement(){Id=ad.Id,Keyword=ad.Keyword,ShortDescription=ad.ShortDescription,CategoryId=ad.CategoryId});	
						ButtonText=saveReturn>0?"Delete It From My List":"Bring It In";
					}
				});
			buttonText=inLocalDB(Ad.Id)?"Delete It From My List":"Bring It In";
			WebViewShow = false;
			AddShow = false;
			IndicatorShow = true;
		}
		private string buttonText;
		public string ButtonText { get{ return buttonText; } set{
				if (buttonText != value)
				{
					buttonText = value;
					OnPropertyChanged("ButtonText");
				}
			}
		}
		private bool addShow;
		public bool AddShow { get{ return addShow; } set{
				if (addShow != value)
				{
					addShow = value;
					OnPropertyChanged("AddShow");
				}
			}
		}
		private bool connectShow;
		public bool ConnectShow { get{ return connectShow; } set{
				if (connectShow != value)
				{
					connectShow = value;
					OnPropertyChanged("ConnectShow");
				}
			}
		}

		public String URL{ get; set;}

		private bool webViewShow;
		public bool WebViewShow { get{ return webViewShow; } set{
				if (webViewShow != value)
				{
					webViewShow = value;
					OnPropertyChanged("WebViewShow");
				}
			}
		}
		private bool indicatorShow;
		public bool IndicatorShow { get{ return indicatorShow; } set{
				if (indicatorShow != value)
				{
					indicatorShow = value;
					OnPropertyChanged("IndicatorShow");
				}
			}
		}

		private bool inLocalDB(int id){
			return _db.GetAd (id) != null ? true : false;
		}
		protected virtual void OnPropertyChanged(string propertyName)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this,
					new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}

