namespace LibraryManagementSystem.Domain.Models.User;

#region User Model

public class UserModel
{
	public string UserName { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string PhoneNumber { get; set; } = null!;

	public string Address { get; set; } = null!;
}

#endregion