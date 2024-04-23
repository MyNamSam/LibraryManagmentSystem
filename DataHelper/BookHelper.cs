using SQLite;
using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.DataHelper
{
    public class BookHelper : IBookHelper

    {
        private SQLiteAsyncConnection BookDBconnection;
        public BookHelper() => SetupBookDatabase();
        private async void SetupBookDatabase() 
        {
            if (BookDBconnection is null) 
            {
                string DBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "DemoBookDB.db3");
                BookDBconnection = new SQLiteAsyncConnection(DBPath);
                await BookDBconnection.CreateTableAsync<Book>();
            }
        }

        public async Task<ServiceResponse> AddOrUpdateBook(Book book)
        {
            if (book is null) return ErrorMessage(-1);
            if (book.ID is 0)
            {
                int respondID = await BookDBconnection.InsertAsync(book);
                return ConfirmationMessage(respondID);
            }
            int updateRespondCode = await BookDBconnection.UpdateAsync(book); return ConfirmationMessage(updateRespondCode);
        }

        public async Task<ServiceResponse> DeleteBook(Book book)
        {
            if(book.ID > 0)
            {
                var result = await GetBook(book.ID);
                if (result is null) return ErrorMessage(book.ID);

                int respondID = await BookDBconnection?.DeleteAsync(book);
                return ConfirmationMessage(respondID);
            }
            return ErrorMessage(-1);
        }
        public async Task<Book> GetBook(int id) => await BookDBconnection.Table<Book>().Where(_ => _.ID == id).FirstOrDefaultAsync();

        public async Task<List<Book>> GetBooks() => await BookDBconnection.Table<Book>().ToListAsync();

        private static ServiceResponse ConfirmationMessage(int respondID) => new() { Flag = true, DatabaseRespondValue = respondID, Message = "Process Completed Successfully." };
        private static ServiceResponse ErrorMessage(int respondID) => new() { Flag = false, DatabaseRespondValue = respondID, Message = "Error... Please try again." };
    }
}
