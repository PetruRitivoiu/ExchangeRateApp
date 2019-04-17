using ExchangeRate.ViewModel;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExchangeRate.View
{
	// Learn more about making custom code visible in the Xamarin.Forms previewer
	// by visiting https://aka.ms/xamarinforms-previewer
	[DesignTimeVisible(true)]
	public partial class HistoryView : ContentPage
	{
		private HistoryViewModel ViewModel { get; set; }

		public HistoryView(HistoryViewModel viewModel)
		{
			InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			ViewModel = viewModel;
			BindingContext = ViewModel;
		}
	}
}
