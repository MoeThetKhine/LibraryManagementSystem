using LibraryManagementSystem.Domain.Features.Transaction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers.Transaction
{
	[Route("api/[controller]")]
	[ApiController]
	public class TransactionController : ControllerBase
	{
		private readonly TransactionService _transactionService;

		public TransactionController(TransactionService transactionService)
		{
			_transactionService = transactionService;
		}

		[HttpGet]
		public async Task<IActionResult> GetTransactionAsync()
		{
			var result = await _transactionService.GetTransactionAsync();
			return Ok(result);
		}
	}
}
