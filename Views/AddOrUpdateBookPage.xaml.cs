using LibraryManagmentSystem.ViewModels;

namespace LibraryManagmentSystem.Views;

public partial class AddOrUpdateBookPage : ContentPage
{
	public AddOrUpdateBookPage(AddOrUpdateBookPageViewModel addOrUpdateBookPageViewModel)
	{
		InitializeComponent();
		BindingContext = addOrUpdateBookPageViewModel;
	}
}