﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers.Book
{
	[Route("api/[controller]")]
	[ApiController]
	public class BookController : ControllerBase
	{
		private readonly BookService _bookService;

		public BookController(BookService bookService)
		{
			_bookService = bookService;
		}

		[HttpGet]
		public async Task<IActionResult> GetBookAsync()
		{
			var result = await _bookService.GetBookAsync();
			return Ok(result);
		}
	}
}
