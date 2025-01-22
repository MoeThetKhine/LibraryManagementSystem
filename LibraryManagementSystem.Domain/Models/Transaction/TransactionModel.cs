namespace LibraryManagementSystem.Domain.Models.Transaction;

#region Transaction Model

public class TransactionModel
{
	public string UserName { get; set; } = null!;

	public string BookId { get; set; } = null!;

	public DateTime BorrowDate { get; set; }

	public DateTime DueDate { get; set; }

	public DateTime ReturnDate { get; set; }

	public decimal Fine { get; set; }

	public int Qty { get; set; }

	public decimal TotalAmount { get; set; }

	public int DaysLate { get; set; }
}

#endregion