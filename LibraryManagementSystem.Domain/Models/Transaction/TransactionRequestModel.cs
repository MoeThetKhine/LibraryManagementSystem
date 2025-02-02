﻿namespace LibraryManagementSystem.Domain.Models.Transaction;

#region Transaction Request Model

public class TransactionRequestModel
{
	public string UserName { get; set; } = null!;

	public string BookId { get; set; } = null!;

	public DateTime BorrowDate { get; set; }

	public DateTime DueDate { get; set; }

	public DateTime ReturnDate { get; set; }

	public int Qty { get; set; }

}

#endregion