namespace LibraryManagementSystem.Domain.Models.Category;

#region Category Model

public class CategoryRequestModel
{
	public string CategoryName { get; set; } = null!;

	public string? Description { get; set; }
}

#endregion
