﻿namespace LibraryManagementSystem.Api.Controllers.User;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
	private readonly UserService _userService;

	public UserController(UserService userService)
	{
		_userService = userService;
	}

	#region Login User

	[HttpPost("login")]
	public async Task<IActionResult> LoginUserAsync([FromForm] LoginUserModel loginUser)
	{
		var result = await _userService.LoginUserAsync(loginUser);
		return Ok(result);
	}

	#endregion

	#region Register User

	[HttpPost("register")]
	public async Task<IActionResult> RegisterUserAsync([FromForm] UserRequestModel userRequest)
	{
		var result = await _userService.RegisterUserAsync(userRequest);
		return Ok(result);
	}

	#endregion

	#region Get Member

	[HttpGet("Get-member")]
	public async Task<IActionResult> GetMemberAsync()
	{
		var result = await _userService.GetMemberAsync();
		return Ok(result);
	}

	#endregion

	#region User Logout

	[HttpPost("logout")]
	public async Task<IActionResult> UserLogoutAsync([FromForm] LogoutModel logoutModel)
	{
		var result = await _userService.UserLogoutAsync(logoutModel);
		return Ok(result);
	}

	#endregion

	#region Update User Async

	[HttpPatch("update/{email}")]
	public async Task<IActionResult> UpdateUserAsync([FromRoute] string email,UpdateUserProfile updateUserProfile)
	{
		
		var result = await _userService.UpdateUserProfileAsync(email, updateUserProfile);
		return Ok(result);
	}

	#endregion

}
