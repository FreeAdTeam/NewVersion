using System;
using SQLite.Net;
using Xamarin.Forms;
using PhoneApp.Models;
using System.Linq;
using System.Collections.Generic;
namespace PhoneApp
{
	public class LocalData
	{
		private SQLiteConnection database;
		static object locker=new object();
		public LocalData ()
		{
			database = DependencyService.Get<ISQLite> ().GetConnection ();
			database.CreateTable<Advertisement> ();
			database.CreateTable<UserInfo> ();
		}
		#region user
		public int SaveUserInfo(UserInfo userInfo)
		{
			lock (locker) 
			{
				if (userInfo.Id != 0) {
					database.Update (userInfo);
					return userInfo.Id;
				} else {
					return database.Insert (userInfo);
				}

			}
		}
		public UserInfo GetUserInfo(){
			lock (locker) {
				return database.Table<UserInfo> ().Where(d=>true).FirstOrDefault ();
			}
		}
		#endregion
		#region ad
		public int SaveAd(Advertisement ad){
			lock (locker) 
			{
					return database.Insert (ad);
			}
		}
		public IEnumerable<Advertisement> GetAds(){
			lock (locker) {
				return database.Table<Advertisement> ().Where (d=>true).ToList ();
			}
		}
		public int DeleteAd(int id){
			lock (locker) {
				//var delItem=database.Table<Advertisement>().Where(d=>d.Id==id).FirstOrDefault();
				return database.Table<Advertisement>().Delete(d=>d.Id==id);
			}
		}
		public Advertisement GetAd(int id){
			lock (locker) {
				return database.Table<Advertisement>().Where(d=>d.Id==id).FirstOrDefault();
			}
		}
		#endregion
	}
}

