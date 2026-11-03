using LibraryManagement.Models;

namespace LibraryManagement.Repository.Interface
{
    public interface IBooksRepository
    {
        Task<IReadOnlyList<Books>> GetAllAsync();
        Task<Books?> GetByIdAsync(int id);
        Task<IReadOnlyList<Books>> SearchAsync(string keyword);
        Task<Books> AddAsync(Books book);
        Task UpdateAsync(Books book);
        Task DeleteAsync(Books book);
        Task<bool> IssueAsync(int id);
        Task<Statistics> GetStatisticsAsync();
    }
}
