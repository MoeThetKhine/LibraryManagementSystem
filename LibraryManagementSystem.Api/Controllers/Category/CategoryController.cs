﻿namespace LibraryManagementSystem.Api.Controllers.Category;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
	private readonly CategoryService _categoryService;

	public CategoryController(CategoryService categoryService)
	{
		_categoryService = categoryService;
	}

	#region Create Category Async

	[HttpPost]
	public async Task<IActionResult> CreateCategoryAsync(CategoryRequestModel requestModel)
	{
		var result = await _categoryService.CreateCategoryAsync(requestModel);
		return Ok(result);
	}

	#endregion

	#region Get Category Async

	[HttpGet]
	public async Task<IActionResult> GetCategoryAsync()
	{
		var result = await _categoryService.GetCategoryAsync();
		return Ok(result);
	}

	#endregion

}
