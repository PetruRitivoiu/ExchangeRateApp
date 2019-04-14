using System;
using System.ComponentModel;
using Xamarin.Forms;
using ExchangeRate.View;
using ExchangeRate.ViewModel;

namespace ExchangeRate
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(true)]
    public partial class MainView : ContentPage
    {
        private MainViewModel ViewModel { get; set; }

        public MainView()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            ViewModel = new MainViewModel();
            BindingContext = ViewModel;
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HistoryView(new HistoryViewModel()));
        }
    }
}
