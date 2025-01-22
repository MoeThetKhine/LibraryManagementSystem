namespace LibraryManagementSystem.Database.AppDbContextModels;

public partial class TblReturn
{
    public string ReturnId { get; set; } = null!;

    public string BorrowId { get; set; } = null!;

    public DateTime ReturnDate { get; set; }

    public int DaysLate { get; set; }

    public decimal Fine { get; set; }

    public decimal TotalAmount { get; set; }
}
