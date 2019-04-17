using ExchangeRate.Model;
using SQLite;
using Xamarin.Forms;

namespace ExchangeRate.Data
{
	public class Database
	{
		public Database()
		{
			Connection =
				DependencyService.Get<IDatabaseConnection>().
				DbConnection();
		}

		public SQLiteAsyncConnection Connection { get; }
	}
}
