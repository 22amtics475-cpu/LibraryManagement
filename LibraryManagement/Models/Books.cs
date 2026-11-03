using System.ComponentModel.DataAnnotations;

namespace LibraryManagement.Models
{
    public class Books
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; } = string.Empty;

        [Required]
        public string Author { get; set; } = string.Empty;

        [Required]
        public string Publisher { get; set; } = string.Empty;

        [Required]
        public string Category { get; set; } = string.Empty;

        [Range(0, int.MaxValue)]
        public int AvailableCopies { get; set; }
    }
}
