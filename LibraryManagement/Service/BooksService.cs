using LibraryManagement.Models;
using LibraryManagement.Repository.Interface;
using LibraryManagement.Service.Interface;

namespace LibraryManagement.Service
{
    public class BooksService : IBooksService
    {
        private readonly IBooksRepository _repository;

        public BooksService(IBooksRepository repository)
        {
            _repository = repository;
        }

        public Task<IReadOnlyList<Books>> GetAllAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Books?> GetByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task<IReadOnlyList<Books>> SearchAsync(string keyword)
        {
            return _repository.SearchAsync(keyword);
        }

        public Task<Books> AddAsync(Books book)
        {
            return _repository.AddAsync(book);
        }

        public async Task<bool> UpdateAsync(int id, Books book)
        {
            var existingBook = await _repository.GetByIdAsync(id);

            if (existingBook == null)
                return false;

            existingBook.Title = book.Title;
            existingBook.Author = book.Author;
            existingBook.Publisher = book.Publisher;
            existingBook.Category = book.Category;
            existingBook.AvailableCopies = book.AvailableCopies;

            await _repository.UpdateAsync(existingBook);
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var book = await _repository.GetByIdAsync(id);

            if (book == null)
                return false;

            await _repository.DeleteAsync(book);
            return true;
        }

        public Task<bool> IssueAsync(int id)
        {
            return _repository.IssueAsync(id);
        }

        public Task<Statistics> GetStatisticsAsync()
        {
            return _repository.GetStatisticsAsync();
        }

        public async Task<IReadOnlyList<Books>> GetLowStockAsync()
        {
            var books = await _repository.GetAllAsync();
            return books.Where(book => book.AvailableCopies < 3).ToList();
        }
    }
}
