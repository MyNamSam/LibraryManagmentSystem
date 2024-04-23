using CommunityToolkit.Mvvm.ComponentModel;
using LibraryManagmentSystem.Models;
using LibraryManagmentSystem.DataHelper;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using LibraryManagmentSystem.CustomControls;

namespace LibraryManagmentSystem.ViewModels
{
    [QueryProperty(nameof(AddBookModel), "UpdateBookData")]
    public partial class AddOrUpdateBookPageViewModel : AddBookBaseViewModel
    {
        public ObservableCollection<Error> Errors { get; set; } = new();

        [ObservableProperty]
        private Book _addBookModel;

        [ObservableProperty]
        private bool _showErrors;

        [ObservableProperty]
        ImageSource _imageSourceFile;

        private readonly IBookHelper bookHelper;
        public AddOrUpdateBookPageViewModel(IBookHelper bookHelper)
        {
            this.bookHelper = bookHelper;
            Title = "Add Your Book:";
            AddBookModel = new Book();
        }

        [RelayCommand]
        private async Task SelectImage()
        {
            var image = await FilePicker.PickAsync(new PickOptions
            {
                PickerTitle = "Select Book Image",
                FileTypes = FilePickerFileType.Images
            });
            if (image == null) return;

            byte[] imageByte;
            var newFile = Path.Combine(FileSystem.CacheDirectory, image.FileName);
            var stream = await image.OpenReadAsync();
            using (MemoryStream memory = new())
            {
                stream.CopyTo(memory);
                imageByte = memory.ToArray();
            }
            //converting to base64string
            var convertedImage = Convert.ToBase64String(imageByte);
            AddBookModel.Image = convertedImage;

            //converting from base64string to image
            var imgFromBase64 = Convert.FromBase64String(convertedImage);
            MemoryStream memoryStream = new(imgFromBase64);
            ImageSourceFile = ImageSource.FromStream(() => memoryStream);
        }


        [RelayCommand]
        private async Task SaveData()
        {
            Errors.Clear();
            if (!ValidateModel(AddBookModel)) return;

            if (Errors.Any() || Errors.Count > 0)
            {
                ShowErrors = true; return;
            }

            var result = await bookHelper.AddOrUpdateBook(AddBookModel);
            if (result.Flag)
            {
                MakeToast(result.Message);
                AddBookModel = new Book();
                return;
            }
            Errors.Clear();
            Errors.Add(new Error() { Property = "Alert: ", Value = result.Message });
            ShowErrors = true;
            return;
        }

        private bool ValidateModel(Book validateBook)
        {
            if (validateBook.Title is null)
                Errors.Add(new Error() { Property = "Title: ", Value = "Book Title cannot be empty" });

            if (validateBook.Author is null)
                Errors.Add(new Error() { Property = "Author: ", Value = "Enter, \"Unknown\", if author is unavailable" });

            if (validateBook.Description is null)
                Errors.Add(new Error() { Property = "Description: ", Value = "Please enter the discription" });
            else 
            {
                if (validateBook.Description.Length < 1)
                    Errors.Add(new Error() { Property = "Description: ", Value = "Please enter something in the description" });
            }
            
            if (validateBook.Image is null)
                Errors.Add(new Error() { Property = "Image: ", Value = "Book Image cannot be empty" });

            return true;
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
        private async Task NavigateToHome() => await Shell.Current.GoToAsync("..", true);
        
    }
}
