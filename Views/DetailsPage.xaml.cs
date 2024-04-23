using LibraryManagmentSystem.ViewModels;

namespace LibraryManagmentSystem.Views;

public partial class DetailsPage : ContentPage
{
	public DetailsPage(DetailsPageViewModel detailsPageViewModel)
	{
		InitializeComponent();
		BindingContext = detailsPageViewModel;
	}
}