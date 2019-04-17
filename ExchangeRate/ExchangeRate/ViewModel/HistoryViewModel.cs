using ExchangeRate.Services;
using ExchangeRate.ViewModel.ViewObjects;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ExchangeRate.ViewModel
{
	public class HistoryViewModel : INotifyPropertyChanged
	{
		private ExchangeRateDb mExchangeRateDb;
		public ObservableCollection<HistoryRateViewObject> Rates { get; set; }
		public string ViewHistoryLabelText => "View history";

		public event PropertyChangedEventHandler PropertyChanged;

		public HistoryViewModel()
		{
			Rates = new ObservableCollection<HistoryRateViewObject>();
			mExchangeRateDb = new ExchangeRateDb();
			initialize();
		}

		private void initialize()
		{
			var rows = mExchangeRateDb.GetAsync().Result;

			foreach (var row in rows)
			{
				Rates.Add(new HistoryRateViewObject
				{
					Base = row.Base,
					Date = row.Date,
					Rate = new RateViewObject
					{
						Name = nameof(row.Rates.USD),
						Rate = row.Rates.USD
					}
				});

				Rates.Add(new HistoryRateViewObject
				{
					Base = row.Base,
					Date = row.Date,
					Rate = new RateViewObject
					{
						Name = nameof(row.Rates.GBP),
						Rate = row.Rates.GBP
					}
				});

				Rates.Add(new HistoryRateViewObject
				{
					Base = row.Base,
					Date = row.Date,
					Rate = new RateViewObject
					{
						Name = nameof(row.Rates.RON),
						Rate = row.Rates.RON
					}
				});

			}
		}


	}
}
