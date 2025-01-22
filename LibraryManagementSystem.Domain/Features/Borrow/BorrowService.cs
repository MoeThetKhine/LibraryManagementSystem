using LibraryManagementSystem.Domain.Models.Borrow;

namespace LibraryManagementSystem.Domain.Features.Borrow
{
	public class BorrowService
	{
		private readonly AppDbContext _appDbContext;

		public BorrowService(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<Result<IEnumerable<BorrowModel>>> GetBorrowListAsync()
		{
			Result<IEnumerable<BorrowModel>> result;

			try
			{
				var borrow = await _appDbContext.TblBorrows.AsNoTracking()
					.ToListAsync();

				var lst = borrow.Select(x => new BorrowModel { 
				UserId = x.UserId,
				BookId = x.BookId,
				BorrowDate = x.BorrowDate,
				DueDate = x.DueDate,
				Qty = x.Qty,
				}).ToList();


				if (!lst.Any())
				{
					result = Result<IEnumerable<BorrowModel>>.ValidationError("No Return Found.");
				}
				result = Result<IEnumerable<BorrowModel>>.Success(lst);
			}
			catch (Exception ex)
			{
				result = Result<IEnumerable<BorrowModel>>.ValidationError(ex.Message);
			}
			return result;
		}

		public async Task<Result<IEnumerable<BorrowModel>>> GetBorrowListByIdAsync(string id)
		{
			Result<IEnumerable<BorrowModel>> result;

			try
			{
				var borrow = await _appDbContext.TblBorrows.AsNoTracking()
					.Where(x => x.BorrowId == id).ToListAsync();

				var lst = borrow.Select(x => new BorrowModel
				{
					UserId = x.UserId,
					BookId = x.BookId,
					BorrowDate = x.BorrowDate,
					DueDate = x.DueDate,
					Qty = x.Qty,
				}).ToList();

				if (!lst.Any())
				{
					result = Result<IEnumerable<BorrowModel>>.ValidationError("No Return Found.");
				}
				result = Result<IEnumerable<BorrowModel>>.Success(lst);
			}
			catch (Exception ex)
			{
				result = Result<IEnumerable<BorrowModel>>.ValidationError(ex.Message);
			}
			return result;
		}

	}
}
