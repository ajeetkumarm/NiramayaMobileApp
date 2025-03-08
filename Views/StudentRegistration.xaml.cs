using Kadam.ViewModels;

namespace Kadam.Views;

public partial class StudentRegistration : ContentPage
{
    public StudentRegistration(StudentRegistrationViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void OnPersonalTabClicked(object sender, EventArgs e)
    {
        // Handle the Personal tab click event
        // For example, you can set the visibility of different sections
        // or change the background color of the selected tab
    }

    private void OnEducationTabClicked(object sender, EventArgs e)
    {
        // Handle the Education tab click event
    }

    private void OnFamilyTabClicked(object sender, EventArgs e)
    {
        // Handle the Family tab click event
    }
}