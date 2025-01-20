namespace LibraryManagementSystem.Domain.Models.Category;

#region Category Model

public class CategoryModel
{
	public string CategoryId { get; set; } = null!;

	public string CategoryName { get; set; } = null!;

	public string? Description { get; set; }
}

#endregion
