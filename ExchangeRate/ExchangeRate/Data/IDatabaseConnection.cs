namespace ExchangeRate.Data
{
	public interface IDatabaseConnection
	{
		SQLite.SQLiteAsyncConnection DbConnection();
	}
}
