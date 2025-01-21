namespace LibraryManagementSystem.Api.Controllers.Book;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
	private readonly BookService _bookService;

	public BookController(BookService bookService)
	{
		_bookService = bookService;
	}

	#region Get Book Async

	[HttpGet]
	public async Task<IActionResult> GetBookAsync()
	{
		var result = await _bookService.GetBookAsync();
		return Ok(result);
	}

	#endregion

	#region Get Books By Category Async

	[HttpGet("CategoryName")]
	public async Task<IActionResult> GetBooksByCategoryAsync(string categoryName)
	{
		var result = await _bookService.GetBooksByCategoryAsync(categoryName);
		return Ok(result);
	}

	#endregion

	#region Create Book Async

	[HttpPost]
	public async Task<IActionResult> CreateBookAsync([FromForm]BookModel bookModel)
	{
		var result = await _bookService.CreateBookAsync(bookModel);
		return Ok(result);
	}

	#endregion

	#region Update Book Async

	[HttpPut("{isbn}")]
	public async Task<IActionResult> UpdateBookAsync(string isbn, BookReponseModel responseModel)
	{
		var result = await _bookService.UpdateBookAsync(isbn, responseModel);
		return Ok(result);
	}

	#endregion

}
