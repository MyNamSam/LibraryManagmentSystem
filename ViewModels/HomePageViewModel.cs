using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using LibraryManagmentSystem.DataHelper;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.Views;
using MvvmHelpers;

namespace LibraryManagmentSystem.ViewModels
{
    public partial class HomePageViewModel : AddBookBaseViewModel
    {
        [ObservableProperty]
        private bool _gridVisibility;
        public HomePageViewModel(IBookHelper bookHelper)
        {
            this.bookHelper = bookHelper;
            Title = "Welcome to the Library Managment System!";
        }

        private readonly IBookHelper bookHelper;
        public ObservableRangeCollection<Book> Books { get; set; } = new();

        [RelayCommand]
        private async Task LoadBookFromDatabase()
        {
            GridVisibility = false;
            await Task.Delay(1000);
            var results = await bookHelper.GetBooks();
            if (results.Count > 1)
            {
                Books.Clear();

                foreach (var book in results)
                {
                    string subString = book.Description.Length > 60 ? book.Description.Substring(0, 60) + "..." : book.Description;
                    Books.Add(new Book() { ID = book.ID, Title = book.Title, Author = book.Author, Description = subString, Image = book.Image });
                }
            }
            GridVisibility = true;
        }

        [RelayCommand]
        private async Task NavigateToAddBookPage() => await Shell.Current.GoToAsync(nameof(AddOrUpdateBookPage), true);

        [RelayCommand]
        private async Task NavigateToDetails(Book bookModel)
        {
            if (bookModel is null) return;

            var desc = await bookHelper.GetBook(bookModel.ID);
            var navigationParameter = new Dictionary<string, object>();
            navigationParameter.Add("ViewBookDetails", desc);
            await Shell.Current.GoToAsync(nameof(DetailsPage), navigationParameter);
        }

        [RelayCommand]
        private async Task DeleteBookData(Book bookToBeDeleted)
        {
            bool answer = await Shell.Current.DisplayAlert("Confirm Delete?", $"Are you sure you wanna delete {bookToBeDeleted.Title}?", "Yes", "No");
            if (answer)
            {
                var result = await bookHelper.DeleteBook(bookToBeDeleted);
                MakeToast(result.Message);

                Books.Remove(bookToBeDeleted);
                await LoadBookFromDatabase();
            }
        }

        private static async void MakeToast(string message)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Long;
            double fontSize = 15;
            var toast = Toast.Make(message, duration, fontSize);
            await toast.Show(cancellationTokenSource.Token);
        }

        [RelayCommand]
        private async Task UpdateBookData(Book bookToBeUpdated)
        {
            bool answer = await Shell.Current.DisplayAlert("Confirm Update?", $"Are you sure you wanna update: {bookToBeUpdated.Title} ?", "Yes", "No");
            if (answer)
            {
                var desc = await bookHelper.GetBook(bookToBeUpdated.ID);
                var navigationParameter = new Dictionary<string, object>();
                navigationParameter.Add("UpdateBookData", desc);
                await Shell.Current.GoToAsync(nameof(AddOrUpdateBookPage), navigationParameter);
            }
        }
    }
}
