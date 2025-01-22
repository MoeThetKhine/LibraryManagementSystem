using LibraryManagementSystem.Domain.Models.Borrow;

namespace LibraryManagementSystem.Api.Controllers.Borrow;

[Route("api/[controller]")]
[ApiController]
public class BorrowController : ControllerBase
{
	private readonly BorrowService _borrowService;

	public BorrowController(BorrowService borrowService)
	{
		_borrowService = borrowService;
	}

	#region Get Borrow List Async

	[HttpGet]
	public async Task<IActionResult> GetBorrowListAsync()
	{
		var result = await _borrowService.GetBorrowListAsync();
		return Ok(result);
	}

	#endregion

	#region Get Borrow List By Id Async

	[HttpGet("{id}")]
	public async Task<IActionResult> GetBorrowListByIdAsync(string id)
	{
		var result = await _borrowService.GetBorrowListByIdAsync(id);
		return Ok(result);
	}

	#endregion

	[HttpPost]
	public async Task<IActionResult> CreateBorrowAsync([FromBody] BorrowModel borrowModel)
	{
		var result = await _borrowService.CreateBorrowAsync(borrowModel);
		return Ok(result);
	}

}
