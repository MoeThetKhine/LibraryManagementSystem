using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Models
{
	public class Result<T>
	{
		public bool IsSuccess { get; set; }
		public bool IsError { get { return !IsSuccess; } }
		public bool IsValidationError { get { return Type == EnumRespType.ValidationError; } }
		public bool IsSystemError { get { return Type == EnumRespType.SystemError; } }
		private EnumRespType Type { get; set; }
		public T Data { get; set; }
		public string Message { get; set; }
	}
}
