using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.ViewModels;

namespace LibraryManagmentSystem.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel homePageViewModel;
	public HomePage(HomePageViewModel homePageViewModel)
	{
		InitializeComponent();
		BindingContext = homePageViewModel;
        this.homePageViewModel = homePageViewModel;
    }

    protected override void OnAppearing()
    {
        homePageViewModel.LoadBookFromDatabaseCommand.Execute(this);
    }
}
