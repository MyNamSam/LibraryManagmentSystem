using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.Models;
using MvvmHelpers.Commands;
using MvvmHelpers.Interfaces;

namespace LibraryManagmentSystem.ViewModels
{
    [QueryProperty(nameof(BookModel), "ViewBookDetails")]
    public partial class DetailsPageViewModel : AddBookBaseViewModel
    {
        [ObservableProperty]
        private Book _bookModel;
    }

    public partial class DetailsPageViewModel
    {
        public IAsyncCommand NavigateToHomeCommand { get; }

        public DetailsPageViewModel()
        {
            NavigateToHomeCommand = new AsyncCommand(NavigateToHome);
        }

        private async Task NavigateToHome()
        {
            await Shell.Current.GoToAsync("//MainPage");
        }
    }
}
