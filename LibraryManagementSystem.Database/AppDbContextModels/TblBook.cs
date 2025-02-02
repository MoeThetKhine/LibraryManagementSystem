﻿namespace LibraryManagementSystem.Database.AppDbContextModels;

#region TblBook

public partial class TblBook
{
    public string BookId { get; set; } = null!;

    public string Title { get; set; } = null!;

    public string Author { get; set; } = null!;

    public string Isbn { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public int Qty { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }
}

#endregion