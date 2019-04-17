﻿using System.Windows.Input;
using Xamarin.Forms;
using ExchangeRate.Services;
using ExchangeRate.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Collections.ObjectModel;
using ExchangeRate.ViewModel.ViewObjects;
using System.Threading.Tasks;

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
			var content = await mExchangeRateApi.GetAsync("https://api.exchangeratesapi.io/latest");
			BaseDescription = $"{nameof(content.Base)}: {content.Base}";
			DateDescription = $"{nameof(content.Date)}: {content.Date}";

			Rates.Add(new RateViewObject() { Name = nameof(content.Rates.USD), Rate = content.Rates.USD });
			Rates.Add(new RateViewObject() { Name = nameof(content.Rates.GBP), Rate = content.Rates.GBP });
			Rates.Add(new RateViewObject() { Name = nameof(content.Rates.RON), Rate = content.Rates.RON });

			await mExchangeRateDb.AddAsync(content);
		}
	}
}
