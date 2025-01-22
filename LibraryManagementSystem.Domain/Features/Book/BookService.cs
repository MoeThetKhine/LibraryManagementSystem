namespace LibraryManagementSystem.Domain.Features.Book;

public class BookService
{
	private readonly AppDbContext _appDbContext;

	public BookService(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	#region Get Book Async

	public async Task<Result<IEnumerable<BookModel>>> GetBookAsync()
	{
		Result<IEnumerable<BookModel>> result;

		try
		{

			#region Validation

			var books = await _appDbContext.TblBooks
				.AsNoTracking()
				.ToListAsync();

			if (!books.Any())
			{
				result = Result<IEnumerable<BookModel>>.ValidationError("No Book Found.");
			}

			#endregion

			var lst =  books.Select(book => new BookModel
			{
				Title = book.Title,
				Author = book.Author,
				Isbn = book.Isbn,
				CategoryName = book.CategoryName,
				Qty = book.Qty,
				Price = book.Price
			}).ToList();

			result = Result<IEnumerable<BookModel>>.Success(lst);
		}
		catch (Exception ex)
		{
			result = Result<IEnumerable<BookModel>>.ValidationError(ex.Message);
		}

		return result;
	}

	#endregion

	#region Get Books By Category Async

	public async Task<Result<IEnumerable<BookModel>>> GetBooksByCategoryAsync(string categoryName)
	{
		Result<IEnumerable<BookModel>> result;

		try
		{

			#region Validation

			var books = await _appDbContext.TblBooks
				.AsNoTracking()
				.Where(x => x.CategoryName == categoryName && x.IsActive)
				.ToListAsync();

			if (books is null || !books.Any())
			{
				return Result<IEnumerable<BookModel>>.ValidationError("No books found.");
			}

			#endregion

			var lst = books.Select(book => new BookModel
			{
				Title = book.Title,
				Author = book.Author,
				Isbn = book.Isbn,
				CategoryName = book.CategoryName,
				Qty = book.Qty,
				Price = book.Price
			}).ToList();

			result = Result<IEnumerable<BookModel>>.Success(lst);
		}
		catch (Exception ex)
		{
			result = Result<IEnumerable<BookModel>>.ValidationError($"An error occurred: {ex.Message}");
		}

		return result;
	}

	#endregion

	#region Create Book Async

	public async Task<Result<BookModel>> CreateBookAsync(BookModel bookModel)
	{
		Result<BookModel> result;

		try
		{
			#region Validation

			var bookExist = await _appDbContext.TblBooks
				.AsNoTracking()
				.AnyAsync(x => x.Isbn == bookModel.Isbn || x.CategoryName == bookModel.CategoryName);

			if (bookExist)
			{
				result = Result<BookModel>.ValidationError("Book already exist.");
			}

			if(bookModel is null)
			{
				result = Result<BookModel>.ValidationError("Please Fill Completely");
			}

			#endregion

			var book = new TblBook
			{
				BookId = Guid.NewGuid().ToString(),
				Title = bookModel.Title,
				Author = bookModel.Author,
				Isbn = bookModel.Isbn,
				CategoryName = bookModel.CategoryName,
				Qty = bookModel.Qty,
				Price = bookModel.Price
			};

			await _appDbContext.TblBooks.AddAsync(book);
			await _appDbContext.SaveChangesAsync();

			result = Result<BookModel>.Success(bookModel, "Creating Successful.");
		}
		catch (Exception ex)
		{
			result = Result<BookModel>.ValidationError(ex.Message);
		}

		return result;
	}

	#endregion

	#region Update Book Async

	public async Task<Result<BookReponseModel>> UpdateBookAsync(string isbn, BookReponseModel responseModel)
	{
		Result<BookReponseModel> result;

		try
		{
			var book = await _appDbContext.TblBooks
				.FirstOrDefaultAsync(x => x.Isbn == isbn && x.IsActive);

			if (book is null)
			{
				return Result<BookReponseModel>.ValidationError("Book not found.");
			}

			book.Qty = responseModel.Qty;
			book.Price = responseModel.Price;

			await _appDbContext.SaveChangesAsync();

			result = Result<BookReponseModel>.Success(responseModel, "Book updated successfully.");
		}
		catch (Exception ex)
		{
			result = Result<BookReponseModel>.ValidationError($"An error occurred: {ex.Message}");
		}

		return result;
	}

	#endregion

}
