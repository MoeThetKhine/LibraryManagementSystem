using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Category
{
	public class CategoryModel
	{
		public string CategoryId { get; set; } = null!;

		public string CategoryName { get; set; } = null!;

		public string? Description { get; set; }
	}
}
