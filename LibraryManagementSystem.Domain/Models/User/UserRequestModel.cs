namespace LibraryManagementSystem.Domain.Models.User;

public class UserRequestModel
{
	public string UserName { get; set; } = null!;

	public string Email { get; set; } = null!;

	public string Password { get; set; } = null!;

	public string PhoneNumber { get; set; } = null!;

	public string Address { get; set; } = null!;

}
