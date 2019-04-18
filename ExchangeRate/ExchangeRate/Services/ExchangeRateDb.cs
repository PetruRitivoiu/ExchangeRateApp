using ExchangeRate.Data;
using ExchangeRate.Model;
using SQLite;
using System.Collections.Generic;
using System.Threading.Tasks;
using SQLiteNetExtensionsAsync.Extensions;
using System;
using System.Linq;

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

		public async Task<List<ExchangeRateModel>> GetCurrentRateAsync()
		{
			var previousDay = DateTime.Now.AddDays(-1).ToString("yyyy-MM-dd");
			var currentDay = DateTime.Now.ToString("yyyy-MM-dd");
			return await connection.GetAllWithChildrenAsync<ExchangeRateModel>(x => x.Date == previousDay || x.Date == currentDay);
		}

	}
}
