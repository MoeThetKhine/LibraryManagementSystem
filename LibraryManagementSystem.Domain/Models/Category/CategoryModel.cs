namespace LibraryManagementSystem.Domain.Models.Category
{
	public class CategoryModel
	{
		public string CategoryId { get; set; } = null!;

		public string CategoryName { get; set; } = null!;

		public string? Description { get; set; }
	}
}
