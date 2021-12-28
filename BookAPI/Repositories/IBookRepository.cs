using BookAPI.Models;

namespace BookAPI.Repositories
{
    public interface IBookRepository
    {
        Task<List<Book>> GetListBooks();

        Task<Book?> GetBook(int id);

        Task<Book?> Create(Book book);

        Task<Book?> Update(Book book);

        Task<bool> Delete(int id);
    }
}
