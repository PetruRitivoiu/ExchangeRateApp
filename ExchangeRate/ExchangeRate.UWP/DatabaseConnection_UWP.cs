using SQLite;
using Xamarin.Forms;
using Windows.Storage;
using System.IO;
using ExchangeRate.Data;
using ExchangeRate.UWP;

[assembly: Dependency(typeof(DatabaseConnection_UWP))]
namespace ExchangeRate.UWP
{
	public class DatabaseConnection_UWP : IDatabaseConnection
	{
		public SQLiteAsyncConnection DbConnection()
		{
			var dbName = "Database.db3";
			var path = Path.Combine(ApplicationData.
				Current.LocalFolder.Path, dbName);
			return new SQLiteAsyncConnection(path);
		}
	}
}