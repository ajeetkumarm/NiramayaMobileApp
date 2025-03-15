using NCD.ViewModels;

namespace NCD.Views;

public partial class Registration : ContentPage
{
	public Registration(RegistrationViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

    }
}