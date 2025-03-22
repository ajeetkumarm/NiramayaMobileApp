using Microsoft.Maui.Controls;
//using NirmayaMobileApp.Services.Interfaces;
using NirmayaMobileApp.ViewModels;

namespace NirmayaMobileApp.Views
{
    public partial class BreastCancerScreening : ContentPage
    {
        //public BreastCancerScreening(IBreastCancerScreeningService screeningService)
            public BreastCancerScreening(IBreastCancerScreeningService screeningService)
        {
            InitializeComponent();
            BindingContext = new BreastCancerScreeningViewModel(screeningService);
        }

        private async void OnBackButtonClicked(object sender, EventArgs e)
        {
            if (BindingContext is BreastCancerScreeningViewModel viewModel)
            {
                await viewModel.GoBackCommand.ExecuteAsync(null);
            }
        }
    }
} 