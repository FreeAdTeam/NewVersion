using System;
using Xamarin.Forms;
using System.IO;
using System.Data;
using SQLite.Net;
[assembly:Dependency(typeof(PhoneApp.Droid.SQLite_Android))]
namespace PhoneApp.Droid
{
	public class SQLite_Android:ISQLite
	{
		public SQLite_Android ()
		{
		}
		#region ISQLite implementation
		public SQLite.Net.SQLiteConnection GetConnection ()
		{
			var fileName = "freead.db3";
			var documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal);
			var path = Path.Combine (documentsPath, fileName);

			var platform = new SQLite.Net.Platform.XamarinAndroid.SQLitePlatformAndroid ();
			var connection = new SQLite.Net.SQLiteConnection (platform, path);

			return connection;
		}


		#endregion
	}
}

