using System;
using SQLite.Net;

namespace PhoneApp
{
	public interface ISQLite
	{
		SQLiteConnection GetConnection();
	}
}

