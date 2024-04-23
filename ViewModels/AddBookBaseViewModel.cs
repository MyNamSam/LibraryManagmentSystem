using CommunityToolkit.Mvvm.ComponentModel;

namespace LibraryManagmentSystem.ViewModels
{
    public partial class AddBookBaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private string _title;
    }
}
