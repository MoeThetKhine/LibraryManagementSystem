using LibraryManagementSystem.Domain.Models.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Features.Book
{
	public class BookService
	{
		private readonly AppDbContext _appDbContext;

		public BookService(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<Result<IEnumerable<BookModel>>> GetBookAsync()
		{
			Result<IEnumerable<BookModel>> result;

			try
			{

			}
			catch (Exception ex)
			{
				result = Result<IEnumerable<BookModel>>.ValidationError(ex.Message);
			}

			return result;
		}
	}
}
