using LibraryManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagement.Data
{
    public class Libraryset : DbContext
    {
        public Libraryset(DbContextOptions<Libraryset> options) : base(options)
        {
        }
        public DbSet<Books> Books => Set<Books>();
    }
}
