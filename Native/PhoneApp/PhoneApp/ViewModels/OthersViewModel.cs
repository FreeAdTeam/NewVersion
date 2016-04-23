using System;
using System.ComponentModel;
namespace PhoneApp
{
	public class OthersViewModel:INotifyPropertyChanged
	{
		public event PropertyChangedEventHandler PropertyChanged;
		public String URL{ get; set;}
		public OthersViewModel ()
		{
			WebViewShow = false;
			IndicatorShow = true;
		}
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
		private bool connectShow;
		public bool ConnectShow { get{ return connectShow; } set{
				if (connectShow != value)
				{
					connectShow = value;
					OnPropertyChanged("ConnectShow");
				}
			}
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

