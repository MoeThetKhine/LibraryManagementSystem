using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.Borrow
{
	public class BorrowModel
	{
		public string BorrowId { get; set; } = null!;

		public string UserId { get; set; } = null!;

		public string BookId { get; set; } = null!;

		public DateTime BorrowDate { get; set; }

		public DateTime DueDate { get; set; }

		public int Qty { get; set; }
	}
}
