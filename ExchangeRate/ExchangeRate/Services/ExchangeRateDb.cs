using ExchangeRate.Data;
using ExchangeRate.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteNetExtensionsAsync.Extensions;

namespace ExchangeRate.Services
{
	public class ExchangeRateDb
	{
		Database db;
		SQLiteAsyncConnection connection;

		public ExchangeRateDb()
		{
			db = new Database();
			connection = db.Connection;

			connection.CreateTableAsync<ExchangeRateModel>().Wait();
			connection.CreateTableAsync<Rates>().Wait();
		}

		public Task<List<ExchangeRateModel>> GetAsync()
		{
			return connection.GetAllWithChildrenAsync<ExchangeRateModel>();
		}

		public Task AddAsync(ExchangeRateModel model)
		{
			return connection.InsertWithChildrenAsync(model);
		}


	}
}
