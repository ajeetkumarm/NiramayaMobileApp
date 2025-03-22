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

	private void DistrictPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (_viewModel.DistrictChangedCommand.CanExecute(null))
		{
			_viewModel.DistrictChangedCommand.Execute(null);
		}
	}

	private void BlockPicker_SelectedIndexChanged(object sender, EventArgs e)
	{
		if (_viewModel.BlockChangedCommand.CanExecute(null))
		{
			_viewModel.BlockChangedCommand.Execute(null);
		}
	}
}