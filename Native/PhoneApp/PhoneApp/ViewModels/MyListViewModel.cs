using System;
using System.Collections.Generic;
using PhoneApp.Models;
using PhoneApp;
using System.Linq;
namespace PhoneApp
{
	public class MyListViewModel
	{
		LocalData _db=new LocalData();
		public List<Advertisement> Companies{ get; set;}
		public List<Advertisement> Recruits{ get; set;}
		public List<Advertisement> Rents{ get; set;}
		public List<Advertisement> Products{ get; set;}
		public List<Advertisement> People{ get; set;}
		public MyListViewModel ()
		{
			var data = _db.GetAds ();
			Companies = data.Where (d => d.CategoryId == 2).ToList();
			Recruits = data.Where (d => d.CategoryId == 4).ToList();
			Rents = data.Where (d => d.CategoryId == 6).ToList();
			Products = data.Where (d => d.CategoryId == 7).ToList();
			People = data.Where (d => d.CategoryId == 18).ToList();
		}
	}
}

