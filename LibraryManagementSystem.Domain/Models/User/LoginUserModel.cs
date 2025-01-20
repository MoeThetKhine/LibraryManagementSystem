using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models.User
{
	public class LoginUserModel
	{
		public string Email { get; set; } = null!;

		public string Password { get; set; } = null!;

	}
}
