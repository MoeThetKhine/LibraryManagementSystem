using LibraryManagementSystem.Domain.Models.Transaction;

namespace LibraryManagementSystem.Api.Controllers.Transaction;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : ControllerBase
{
	private readonly TransactionService _transactionService;

	public TransactionController(TransactionService transactionService)
	{
		_transactionService = transactionService;
	}

	#region Get Transaction Async

	[HttpGet]
	public async Task<IActionResult> GetTransactionAsync()
	{
		var result = await _transactionService.GetTransactionAsync();
		return Ok(result);
	}

	#endregion

	#region Get Transaction By Date Async

	[HttpGet("{borrowDate}")]
	public async Task<IActionResult> GetTransactionByDateAsync(DateTime borrowDate)
	{
		var result = await _transactionService.GetTransactionByDateAsync(borrowDate);
		return Ok(result);
	}

	#endregion

	#region Add Transaction Async

	[HttpPost]
	public async Task<IActionResult> AddTransactionAsync(TransactionRequestModel transactionRequestModel)
	{
		var result = await _transactionService.AddTransactionAsync(transactionRequestModel);
		return Ok(result);
	}

	#endregion

}
