using NCD.ViewModels;

namespace NCD.Views;

public partial class Registration : ContentPage
{
	private RegistrationViewModel _viewModel;

	public Registration()
	{
		InitializeComponent();
		_viewModel = new RegistrationViewModel();
		BindingContext = _viewModel;
	}
}