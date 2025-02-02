﻿namespace LibraryManagementSystem.Database.AppDbContextModels;

#region TblBorrow

public partial class TblBorrow
{
    public string BorrowId { get; set; } = null!;

    public string UserId { get; set; } = null!;

    public string BookId { get; set; } = null!;

    public DateTime BorrowDate { get; set; }

    public DateTime DueDate { get; set; }

    public int Qty { get; set; }
}

#endregion