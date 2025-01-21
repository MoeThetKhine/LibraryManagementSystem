﻿namespace LibraryManagementSystem.Domain.Models.User;

#region Update User Profile

public class UpdateUserProfile
{
	public string UserName { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string Password { get; set; } = null!;

	public string UserRole { get; set; } = null!;

	public string PhoneNumber { get; set; } = null!;

	public string Address { get; set; } = null!;

}

#endregion