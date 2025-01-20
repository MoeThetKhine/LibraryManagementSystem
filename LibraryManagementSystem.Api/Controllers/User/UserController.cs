﻿using LibraryManagementSystem.Domain.Features.User;
using LibraryManagementSystem.Domain.Models.User;
using Microsoft.AspNetCore.Mvc;

namespace LibraryManagementSystem.Api.Controllers.User
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		private readonly UserService _userService;

		public UserController(UserService userService)
		{
			_userService = userService;
		}

		[HttpPost("login")]
		public async Task<IActionResult> LoginUserAsync([FromForm]LoginUserModel loginUser)
		{
			var result = await _userService.LoginUserAsync(loginUser);
			return Ok(result);
		}

	}
}
