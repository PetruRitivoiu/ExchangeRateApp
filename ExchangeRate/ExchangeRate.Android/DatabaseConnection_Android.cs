using SQLite;
using System.IO;
using ExchangeRate.Data;
using ExchangeRate.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(DatabaseConnection_Android))]
namespace ExchangeRate.Droid
{
	public class DatabaseConnection_Android : IDatabaseConnection
	{
		public SQLiteAsyncConnection DbConnection()
		{
			var dbName = "Database.db3";
			var path = Path.Combine(System.Environment.
				GetFolderPath(System.Environment.
				SpecialFolder.Personal), dbName);
			return new SQLiteAsyncConnection(path);
		}
	}
}