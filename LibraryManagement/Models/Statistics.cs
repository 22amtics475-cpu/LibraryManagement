namespace LibraryManagement.Models
{
    public class Statistics
    {
        public int TotalBooks { get; set; }
        public int TotalAvailableCopies { get; set; }
        public int LowStockBooks { get; set; }
        public string? TopAuthor { get; set; }
        public string? TopPublisher { get; set; }

    }
}
