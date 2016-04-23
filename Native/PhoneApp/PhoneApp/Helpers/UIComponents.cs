using System;
using Xamarin.Forms;
namespace PhoneApp
{
	public static class UIComponents
	{
		public static ActivityIndicator indicator{
			get{
				return new ActivityIndicator
				{
					VerticalOptions = LayoutOptions.CenterAndExpand,
					BackgroundColor=Color.White,
					IsVisible=true,
					IsRunning=true
				};
			}
		}
		public static WebView GetWebView(string url){
			return new WebView() { HeightRequest = 0, WidthRequest = 1000, Source = url };
		}
	}
}

