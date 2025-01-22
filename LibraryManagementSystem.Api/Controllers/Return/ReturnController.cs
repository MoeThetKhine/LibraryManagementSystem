using LibraryManagementSystem.Domain.Features.Return;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers.Return
{
	[Route("api/[controller]")]
	[ApiController]
	public class ReturnController : ControllerBase
	{
		private readonly ReturnService _returnService;

		public ReturnController(ReturnService returnService)
		{
			_returnService = returnService;
		}

		[HttpGet]
		public async Task<IActionResult> GetReturnListAsync()
		{
			var result = await _returnService.GetReturnListAsync();
			return Ok(result);
		}			
	}
}
