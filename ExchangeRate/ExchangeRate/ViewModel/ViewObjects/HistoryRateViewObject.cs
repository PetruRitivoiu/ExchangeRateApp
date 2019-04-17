namespace ExchangeRate.ViewModel.ViewObjects
{
	public class HistoryRateViewObject
	{
		public string Base { get; set; }
		public string Date { get; set; }

		public string Description => $"{nameof(Base)}: {Base}, {nameof(Date)}: {Date}";

		public RateViewObject Rate { get; set; }

	}
}
