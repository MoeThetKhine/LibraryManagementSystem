namespace LibraryManagementSystem.Domain.Features.Book;

public class BookService
{
	private readonly AppDbContext _appDbContext;

	public BookService(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	public async Task<Result<IEnumerable<BookModel>>> GetBookAsync()
	{
		Result<IEnumerable<BookModel>> result;

		try
		{
			var books = await _appDbContext.TblBooks
				.AsNoTracking()
				.ToListAsync();

			if (!books.Any())
			{
				result = Result<IEnumerable<BookModel>>.ValidationError("No Book Found.");
			}

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
}
