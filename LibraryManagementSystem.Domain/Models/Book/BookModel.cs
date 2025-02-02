﻿namespace LibraryManagementSystem.Domain.Models.Book;

#region Book Model

public class BookModel
{
	public string Title { get; set; } = null!;

	public string Author { get; set; } = null!;

	public string Isbn { get; set; } = null!;

	public string CategoryName { get; set; } = null!;

	public int Qty { get; set; }

	public decimal Price { get; set; }
}

#endregion