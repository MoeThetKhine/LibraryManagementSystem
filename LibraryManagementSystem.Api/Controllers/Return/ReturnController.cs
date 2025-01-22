using LibraryManagementSystem.Domain.Models.Return;

namespace LibraryManagementSystem.Api.Controllers.Return;

[Route("api/[controller]")]
[ApiController]
public class ReturnController : ControllerBase
{
	private readonly ReturnService _returnService;

	public ReturnController(ReturnService returnService)
	{
		_returnService = returnService;
	}

	#region Get Return List Async

	[HttpGet]
	public async Task<IActionResult> GetReturnListAsync()
	{
		var result = await _returnService.GetReturnListAsync();
		return Ok(result);
	}

	#endregion

	#region Get Return List By Id Async

	[HttpGet("{id}")]
	public async Task<IActionResult> GetReturnListByIdAsync(string id)
	{
		var result = await _returnService.GetReturnListByIdAsync(id);
		return Ok(result);
	}

	#endregion

	[HttpPost]
	public async Task<IActionResult> CreateReturnAsync(ReturnModel returnModel)
	{
		var result = await _returnService.CreateReturnAsync(returnModel);
		return Ok(result);
	}

}
