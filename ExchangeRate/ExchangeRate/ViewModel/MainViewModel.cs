using System.Windows.Input;
using Xamarin.Forms;
using ExchangeRate.Services;
using ExchangeRate.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ExchangeRate.ViewModel
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //fields
        private ICommand mRefreshExchangeRateCommand;
        private ExchangeRateModel mCurrentExchangeRate;
        private ExchangeRateService mExchangeRateService;
        private string baseDescription;
        private string dateDescription;

        //constructors
        public MainViewModel()
        {
            mExchangeRateService = new ExchangeRateService();
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
        public string BaseDescription {
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

        //commands
        public ICommand RefreshExchangeRateCommand => mRefreshExchangeRateCommand ?? (mRefreshExchangeRateCommand = new Command(() => updateExchangeRate()));

        public event PropertyChangedEventHandler PropertyChanged;

        protected void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        //methods
        private async void updateExchangeRate()
        {
            var content = await mExchangeRateService.GetAsync();
            BaseDescription = $"{nameof(content.Base)}: {content.Base}";
            DateDescription = $"{nameof(content.Date)}: {content.Date}";
        }
    }
}
