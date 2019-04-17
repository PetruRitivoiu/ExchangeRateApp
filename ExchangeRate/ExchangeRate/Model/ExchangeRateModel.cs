using Newtonsoft.Json;
using SQLite;
using SQLiteNetExtensions.Attributes;

namespace ExchangeRate.Model
{
	[Table("ExchangeRates")]
	public class ExchangeRateModel
	{
		[PrimaryKey, AutoIncrement]
		[JsonIgnore]
		public int Id { get; set; }
		public string Base { get; set; }
		public string Date { get; set; }

		[ForeignKey(typeof(Rates))]
		public int RatesId { get; set; }

		[OneToOne(CascadeOperations = CascadeOperation.All)]
		public Rates Rates { get; set; }
	}
}

