using LibraryManagement.Models;

namespace LibraryManagement.Service.Interface
{
    public interface IBooksService
    {
        Task<IReadOnlyList<Books>> GetAllAsync();
        Task<Books?> GetByIdAsync(int id);
        Task<IReadOnlyList<Books>> SearchAsync(string keyword);
        Task<Books> AddAsync(Books book);
        Task<bool> UpdateAsync(int id, Books book);
        Task<bool> DeleteAsync(int id);
        Task<bool> IssueAsync(int id);
        Task<Statistics> GetStatisticsAsync();
        Task<IReadOnlyList<Books>> GetLowStockAsync();
    }
}
