using LibraryManagement.Data;
using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Repository
{
    public class BooksRepository : IBooksRepository
    {
        private readonly Libraryset _db;

        public BooksRepository(Libraryset db)
        {
            _db = db;
        }

        public async Task<IReadOnlyList<Books>> GetAllAsync()
        {
            return await _db.Books
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task<Books?> GetByIdAsync(int id)
        {
            return await _db.Books.FindAsync(id);
        }

        public async Task<IReadOnlyList<Books>> SearchAsync(string keyword)
        {
            return await _db.Books
                .AsNoTracking()
                .Where(book =>
                    book.Title.Contains(keyword) ||
                    book.Author.Contains(keyword) ||
                    book.Publisher.Contains(keyword))
                .ToListAsync();
        }

        public async Task<Books> AddAsync(Books book)
        {
            _db.Books.Add(book);
            await _db.SaveChangesAsync();
            return book;
        }

        public async Task UpdateAsync(Books book)
        {
            _db.Books.Update(book);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(Books book)
        {
            _db.Books.Remove(book);
            await _db.SaveChangesAsync();
        }

        public async Task<bool> IssueAsync(int id)
        {
            var book = await _db.Books.FindAsync(id);

            if (book == null || book.AvailableCopies <= 0)
            {
                return false;
            }

            book.AvailableCopies--;
            await _db.SaveChangesAsync();
            return true;
        }

        public async Task<Statistics> GetStatisticsAsync()
        {
            var books = await _db.Books.AsNoTracking().ToListAsync();

            var topAuthor = books.GroupBy(book => book.Author).OrderByDescending(group => group.Count()).Select(group => group.Key).FirstOrDefault();

            var topPublisher = books.GroupBy(book => book.Publisher).OrderByDescending(group => group.Count()).Select(group => group.Key).FirstOrDefault();

            return new Statistics
            {
                TotalBooks = books.Count,
                TotalAvailableCopies = books.Sum(book => book.AvailableCopies),
                LowStockBooks = books.Count(book => book.AvailableCopies < 3),
                TopAuthor = topAuthor,
                TopPublisher = topPublisher
            };
        }
    }
}
