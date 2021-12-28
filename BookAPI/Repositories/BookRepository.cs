using BookAPI.Data;
using BookAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace BookAPI.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly BookDbContext _db;

        public BookRepository(BookDbContext db)
        {
            _db = db;
        }

        public async Task<Book?> Create(Book book)
        {
            if (await GetBook(book.ID) != null)
                return null;

            await _db.Books.AddAsync(book);
            await _db.SaveChangesAsync();
            return book;
        }

        public async Task<bool> Delete(int id)
        {
            var book = await GetBook(id);

            if (book == null)
                return false;

            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Book?> GetBook(int id)
        {
            var book = await _db.Books.FirstOrDefaultAsync(b => b.ID == id);

            return book;
        }

        public async Task<List<Book>> GetListBooks()
        {
            return await _db.Books.ToListAsync();
        }

        public async Task<Book?> Update(Book book)
        {
            if (book == null || await _db.Books.FindAsync(book.ID) == null)
                return null;

            var bookUpdate = await GetBook(book.ID);

            bookUpdate.Name = book.Name;
            bookUpdate.Authors = book.Authors;

            await _db.SaveChangesAsync();
            return book;
        }
    }
}
