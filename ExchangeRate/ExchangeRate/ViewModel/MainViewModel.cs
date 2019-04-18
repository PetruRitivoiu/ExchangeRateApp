using System.Windows.Input;
using Xamarin.Forms;
using ExchangeRate.Services;
using ExchangeRate.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ExchangeRate.ViewModel.ViewObjects;
using System.Threading.Tasks;
using System.Linq;
using System;

namespace ExchangeRate.ViewModel
{
	public class MainViewModel : INotifyPropertyChanged
	{
		//fields
		private ICommand mRefreshExchangeRateCommand;
		private ExchangeRateModel mCurrentExchangeRate;
		private ExchangeRateApi mExchangeRateApi;
		private ExchangeRateDb mExchangeRateDb;
		private string baseDescription;
		private string dateDescription;
		private string currentRateCached;

		//constructors
		public MainViewModel()
		{
			mExchangeRateApi = new ExchangeRateApi();
			mExchangeRateDb = new ExchangeRateDb();
			Rates = new ObservableCollection<RateViewObject>();
		}


		//properties
		public string ViewHistoryButtonText => "View history";
		public string ExchangeAppLabel => "Welcome to the ExchangeApp";
		public string RefreshExchangeRateButtonText => "Refresh exchange rate";
		public ExchangeRateModel CurrentExchangeRate
		{
			get
			{
				return mCurrentExchangeRate;
			}
			set
			{
				mCurrentExchangeRate = value;
				RaisePropertyChanged();
			}
		}
		public string BaseDescription
		{
			get => baseDescription;
			set
			{
				baseDescription = value;
				RaisePropertyChanged();
			}
		}
		public string DateDescription
		{
			get => dateDescription;
			set
			{
				dateDescription = value;
				RaisePropertyChanged();
			}
		}
		public string CurrentRateCached
		{
			get => currentRateCached;
			set
			{
				currentRateCached = value;
				RaisePropertyChanged();
			}
		}

		public ObservableCollection<RateViewObject> Rates { get; set; }

		//commands
		public ICommand RefreshExchangeRateCommand => mRefreshExchangeRateCommand ?? (mRefreshExchangeRateCommand = new Command(async () => await updateExchangeRate()));

		public event PropertyChangedEventHandler PropertyChanged;

		protected void RaisePropertyChanged([CallerMemberName] string caller = "")
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs(caller));
			}
		}

		//methods
		private async Task updateExchangeRate()
		{
			var currentRate = (await mExchangeRateDb.GetCurrentRateAsync()).FirstOrDefault();
			if (currentRate == null)
			{
				var content = await mExchangeRateApi.GetAsync("https://api.exchangeratesapi.io/latest");
				BaseDescription = $"{nameof(content.Base)}: {content.Base}";
				DateDescription = $"{nameof(content.Date)}: {content.Date}";

				Rates.Add(new RateViewObject() { Name = nameof(content.Rates.USD), Rate = content.Rates.USD });
				Rates.Add(new RateViewObject() { Name = nameof(content.Rates.GBP), Rate = content.Rates.GBP });
				Rates.Add(new RateViewObject() { Name = nameof(content.Rates.RON), Rate = content.Rates.RON });

				CurrentRateCached = $"Exchange rate for {content.Date} has been cached";

				await mExchangeRateDb.AddAsync(content);
			}
			else
			{
				CurrentRateCached = $"Results retrieved from cache (sqlite)";

				if (!Rates.Any())
				{
					BaseDescription = currentRate.Base;
					DateDescription = currentRate.Date;

					Rates.Add(new RateViewObject() { Name = nameof(currentRate.Rates.USD), Rate = currentRate.Rates.USD });
					Rates.Add(new RateViewObject() { Name = nameof(currentRate.Rates.GBP), Rate = currentRate.Rates.GBP });
					Rates.Add(new RateViewObject() { Name = nameof(currentRate.Rates.RON), Rate = currentRate.Rates.RON });
				}
			}
		}
	}
}
