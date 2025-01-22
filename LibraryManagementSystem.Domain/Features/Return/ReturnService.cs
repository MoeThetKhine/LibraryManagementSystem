namespace LibraryManagementSystem.Domain.Features.Return;

public class ReturnService
{
	private readonly AppDbContext _appDbContext;

	public ReturnService(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	#region Get Return List Async

	public async Task<Result<IEnumerable<ReturnModel>>> GetReturnListAsync()
	{
		Result<IEnumerable<ReturnModel>> result;

		try
		{
			var returns = await _appDbContext.TblReturns
				.AsNoTracking()
				.ToListAsync();

			var lst = returns.Select(x => new ReturnModel
			{
				BorrowId = x.BorrowId,
				ReturnDate = x.ReturnDate,
				DaysLate = x.DaysLate,
				Fine = x.Fine,
				TotalAmount = x.TotalAmount
			}).ToList();

			if (!lst.Any())
			{
				result = Result<IEnumerable<ReturnModel>>.ValidationError("No Return Found.");
			}
			result = Result<IEnumerable<ReturnModel>>.Success(lst);

		}
		catch (Exception ex)
		{
			result = Result<IEnumerable<ReturnModel>>.ValidationError(ex.Message);
		}
		return result;
	}

	#endregion

	#region Get Return List By Id Async

	public async Task<Result<IEnumerable<ReturnModel>>> GetReturnListByIdAsync(string id)
	{
		Result<IEnumerable<ReturnModel>> result;

		try
		{
			var returns = await _appDbContext.TblReturns.AsNoTracking()
				.Where(x=> x.ReturnId == id).ToListAsync();

			var lst = returns.Select (x => new ReturnModel
			{
				BorrowId = x.BorrowId,
				ReturnDate = x.ReturnDate,
				DaysLate = x.DaysLate,
				Fine = x.Fine,
				TotalAmount = x.TotalAmount
			}).ToList();


			if (!lst.Any())
			{
				result = Result<IEnumerable<ReturnModel>>.ValidationError("No Return Found.");
			}
			result = Result<IEnumerable<ReturnModel>>.Success(lst);

		}
		catch (Exception ex)
		{
			result = Result<IEnumerable<ReturnModel>>.ValidationError(ex.Message);
		}
		return result;
	}

	#endregion

	#region Create Return Async

	public async Task<Result<ReturnModel>> CreateReturnAsync(ReturnModel returnModel)
	{
		Result<ReturnModel> result;

		try
		{
			var borrow = await _appDbContext.TblBorrows
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.BorrowId == returnModel.BorrowId);

			var book = await _appDbContext.TblBooks
			.FirstOrDefaultAsync(x => x.BookId == borrow.BookId && x.IsActive);

			if (borrow is null)
			{
				result = Result<ReturnModel>.ValidationError("Borrow Id Not Found.");
			}

			var dueDate = borrow!.DueDate;

			decimal fineRatePerDay = 3000;

			int daysLate = (returnModel.ReturnDate > dueDate) ? (returnModel.ReturnDate - dueDate).Days : 0;

			decimal fine = daysLate > 0 ? daysLate * fineRatePerDay : 0;

			decimal totalAmount = fine + (book.Price * borrow.Qty);

			book.Qty += borrow.Qty;

			var returnEntity = new TblReturn
			{
				ReturnId = Guid.NewGuid().ToString(),
				BorrowId = returnModel.BorrowId,
				ReturnDate = DateTime.UtcNow,
				DaysLate = daysLate,
				Fine = fine,
				TotalAmount = totalAmount
			};

			_appDbContext.TblBooks.Update(book);

			await _appDbContext.TblReturns.AddAsync(returnEntity);
			await _appDbContext.SaveChangesAsync();

			result = Result<ReturnModel>.Success(returnModel);
		}
		catch (Exception ex)
		{
			result = Result<ReturnModel>.ValidationError(ex.Message);
		}
		return result;
	}

	#endregion

}
