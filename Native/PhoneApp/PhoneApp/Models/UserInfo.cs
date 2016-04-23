using System;
using SQLite.Net.Attributes;
namespace PhoneApp
{
	public class UserInfo
	{
		[PrimaryKey,AutoIncrement]
		public int Id { get; set; }
		public string LastName { get; set; }
		public string FirstName { get; set; }
		public string Email { get; set; }
		public string Phone { get; set; }
		public string Mobile { get; set; }
		public bool ReceiveEmail{ get; set; }
		public bool ReceiveNotification{ get; set; }
	}
}

