namespace LibraryManagementSystem.Domain.Features.Borrow;

public class BorrowService
{
	private readonly AppDbContext _appDbContext;

	public BorrowService(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	#region GetBorrowListAsync

	public async Task<Result<IEnumerable<BorrowModel>>> GetBorrowListAsync()
	{
		Result<IEnumerable<BorrowModel>> result;

		try
		{
			#region Validation

			var borrow = await _appDbContext.TblBorrows.AsNoTracking()
				.ToListAsync();

			#endregion

			var lst = borrow.Select(x => new BorrowModel
			{
				BorrowId = x.BorrowId,
				UserId = x.UserId,
				BookId = x.BookId,
				BorrowDate = x.BorrowDate,
				DueDate = x.DueDate,
				Qty = x.Qty,
			}).ToList();

			#region Validation

			if (!lst.Any())
			{
				result = Result<IEnumerable<BorrowModel>>.ValidationError("No Return Found.");
			}

			#endregion

			result = Result<IEnumerable<BorrowModel>>.Success(lst);
		}
		catch (Exception ex)
		{
			result = Result<IEnumerable<BorrowModel>>.ValidationError(ex.Message);
		}
		return result;
	}

	#endregion

	#region GetBorrowListByIdAsync

	public async Task<Result<IEnumerable<BorrowModel>>> GetBorrowListByIdAsync(string id)
	{
		Result<IEnumerable<BorrowModel>> result;

		try
		{
			var borrow = await _appDbContext.TblBorrows.AsNoTracking()
				.Where(x => x.BorrowId == id).ToListAsync();

			var lst = borrow.Select(x => new BorrowModel
			{
				BorrowId = x.BorrowId,
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

	#endregion

	#region CreateBorrowAsync

	public async Task<Result<BorrowModel>> CreateBorrowAsync(BorrowModel borrowModel)
	{
		Result<BorrowModel> result;

		try
		{
			#region Validation

			DateTime borrowDate = borrowModel.BorrowDate;
			DateTime dueDate = borrowModel.DueDate;

			var isBookAvailable = await _appDbContext.TblBooks
				.AsNoTracking()
				.AnyAsync(x => x.BookId == borrowModel.BookId && x.IsActive);

			var isUserAvailable = await _appDbContext.TblUsers
				.AsNoTracking()
				.AnyAsync(x => x.UserId == borrowModel.UserId && x.IsActive);

			var book = await _appDbContext.TblBooks
			.FirstOrDefaultAsync(x => x.BookId == borrowModel.BookId && x.IsActive);

			if (!isUserAvailable)
			{
				result = Result<BorrowModel>.ValidationError("User Not Found.");
			}

			if (dueDate < borrowDate)
			{
				result = Result<BorrowModel>.ValidationError("Due Date should be greater than Borrow Date.");
			}

			if (book.Qty < borrowModel.Qty)
			{
				return Result<BorrowModel>.ValidationError("Insufficient stock available.");
			}

			book.Qty -= borrowModel.Qty;

			#endregion

			var borrow = new TblBorrow
			{
				BookId = Guid.NewGuid().ToString(),
				UserId = borrowModel.UserId,
				BorrowId = borrowModel.BorrowId,
				BorrowDate = borrowModel.BorrowDate,
				DueDate = borrowModel.DueDate,
				Qty = borrowModel.Qty
			};

			_appDbContext.TblBooks.Update(book);

			await _appDbContext.TblBorrows.AddAsync(borrow);
			await _appDbContext.SaveChangesAsync();

			result = Result<BorrowModel>.Success(borrowModel);
		}
		catch (Exception ex)
		{
			result = Result<BorrowModel>.ValidationError(ex.Message);
		}
		return result;
	}

	#endregion

}
