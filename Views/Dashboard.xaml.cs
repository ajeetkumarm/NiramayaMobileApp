using Kadam.Services;

namespace Kadam.Views;

public partial class Dashboard : ContentPage
{
    private readonly IToolbarService _toolbarService;

    public Dashboard(IToolbarService toolbarService)
    {
        InitializeComponent();
        _toolbarService = toolbarService;
        _toolbarService.AddLogoutButton(this); // Use the service to add the logout button
    }
}