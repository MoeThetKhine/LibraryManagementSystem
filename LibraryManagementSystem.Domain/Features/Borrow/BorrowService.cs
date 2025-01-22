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
			var borrow = await _appDbContext.TblBorrows.AsNoTracking()
				.ToListAsync();

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

	public async Task<Result<BorrowModel>> CreateBorrowAsync(BorrowModel borrowModel)
	{
		Result<BorrowModel> result;

		try
		{
			DateTime borrowDate = borrowModel.BorrowDate;

			var borrow = new TblBorrow
			{
				BookId = Guid.NewGuid().ToString(),
				UserId = borrowModel.UserId,
				BorrowId = borrowModel.BorrowId,
				BorrowDate = borrowModel.BorrowDate,
				DueDate = borrowModel.DueDate,
				Qty = borrowModel.Qty
			};

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




}
