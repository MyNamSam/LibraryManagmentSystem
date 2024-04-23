using LibraryManagmentSystem.Models;

namespace LibraryManagmentSystem.DataHelper
{
    public interface IBookHelper
    {
        Task<ServiceResponse> AddOrUpdateBook(Book book);
        Task<ServiceResponse> DeleteBook(Book book);
        Task<List<Book>> GetBooks();
        Task<Book> GetBook(int id);
    }
}
