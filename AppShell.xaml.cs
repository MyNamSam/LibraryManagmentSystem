using LibraryManagmentSystem.Views;

namespace LibraryManagmentSystem
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(AddOrUpdateBookPage), typeof(AddOrUpdateBookPage));
            Routing.RegisterRoute(nameof(DetailsPage), typeof(DetailsPage));


        }
    }
}
