using LibraryManagementSystem.Domain.Features.Borrow;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers.Borrow
{
	[Route("api/[controller]")]
	[ApiController]
	public class BorrowController : ControllerBase
	{
		private readonly BorrowService _borrowService;

		public BorrowController(BorrowService borrowService)
		{
			_borrowService = borrowService;
		}

		[HttpGet]
		public async Task<IActionResult> GetBorrowListAsync()
		{
			var result = await _borrowService.GetBorrowListAsync();
			return Ok(result);
		}
	}
}
