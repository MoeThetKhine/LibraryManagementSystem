namespace LibraryManagementSystem.Domain.Features.Transaction;

public class TransactionService
{
	private readonly AppDbContext _appDbContext;

	public TransactionService(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	#region Get Transaction Async

	public async Task<Result<IEnumerable<TransactionModel>>> GetTransactionAsync()
	{
		Result<IEnumerable<TransactionModel>> result;

		try
		{
			var transactions = await _appDbContext.TblTransactions
				.AsNoTracking()
				.ToListAsync();

			if (!transactions.Any())
			{
				result = Result<IEnumerable<TransactionModel>>.ValidationError("No Transaction Found.");
			}

			var lst = transactions.Select(transaction => new TransactionModel
			{
				UserName = transaction.UserName,
				BookId = transaction.BookId,
				BorrowDate = transaction.BorrowDate,
				DueDate = transaction.DueDate,
				ReturnDate = transaction.ReturnDate,
				Fine = transaction.Fine,
				Qty = transaction.Qty,
				TotalAmount = transaction.TotalAmount,
				//DaysLate = transaction.DaysLate
			}).ToList();

			result = Result<IEnumerable<TransactionModel>>.Success(lst);
		}
		catch (Exception ex)
		{
			result = Result<IEnumerable<TransactionModel>>.ValidationError(ex.Message);
		}

		return result;
	}

	#endregion

	#region Get Transaction By Date Async

	public async Task<Result<TransactionModel>> GetTransactionByDateAsync(DateTime borrowDate)
	{
		Result<TransactionModel> result;

		try
		{
			var transaction = await _appDbContext.TblTransactions
				.AsNoTracking()
				.FirstOrDefaultAsync(x => x.BorrowDate == borrowDate);

			if (transaction is null)
			{
				result = Result<TransactionModel>.ValidationError("Transaction Not Found.");
			}

			var model = new TransactionModel
			{
				UserName = transaction.UserName,
				BookId = transaction.BookId,
				BorrowDate = transaction.BorrowDate,
				DueDate = transaction.DueDate,
				ReturnDate = transaction.ReturnDate,
				Fine = transaction.Fine,
				Qty = transaction.Qty,
				TotalAmount = transaction.TotalAmount,
				//DaysLate = transaction.DaysLate
			};

			result = Result<TransactionModel>.Success(model);
		}
		catch (Exception ex)
		{
			result = Result<TransactionModel>.ValidationError(ex.Message);
		}

		return result;
	}

	#endregion

	#region Add Transaction Async

	public async Task<Result<TransactionRequestModel>> AddTransactionAsync(TransactionRequestModel model)
	{
		Result<TransactionRequestModel> result;

		try
		{
			
			DateTime dueDate = model.DueDate; 
			DateTime borrowDate = model.BorrowDate;

			var isBookAvailable = await _appDbContext.TblBooks
				.AsNoTracking()
				.AnyAsync(x => x.BookId == model.BookId && x.IsActive);

			var isUserAvailable = await _appDbContext.TblUsers
				.AsNoTracking()
				.AnyAsync(x => x.UserName == model.UserName && x.IsActive);

			var book = await _appDbContext.TblBooks
			.FirstOrDefaultAsync(x => x.BookId == model.BookId && x.IsActive);

			if (!isUserAvailable)
			{
				result = Result<TransactionRequestModel>.ValidationError("User Not Found.");
			}

			if(dueDate < borrowDate)
			{
				result = Result<TransactionRequestModel>.ValidationError("Due Date should be greater than Borrow Date.");
			}

			decimal fineRatePerDay = 3000;

			int daysLate = (model.ReturnDate > model.DueDate) ? (model.ReturnDate - model.DueDate).Days : 0;

			decimal fine = daysLate > 0 ? daysLate * fineRatePerDay : 0;

			if (book.Qty < model.Qty)
			{
				return Result<TransactionRequestModel>.ValidationError("Insufficient stock available.");
			}


			decimal totalAmount = fine + (book.Price * model.Qty);

			var transaction = new TblTransaction
			{
				TransactionId = Guid.NewGuid().ToString(),
				UserName = model.UserName,
				BookId = model.BookId,
				BorrowDate = model.BorrowDate,
				DueDate = model.DueDate,
				ReturnDate = model.ReturnDate,
				Fine = fine,
				Qty = model.Qty,
				TotalAmount = totalAmount,
				DaysLate = daysLate
			};

			await _appDbContext.TblTransactions.AddAsync(transaction);
			await _appDbContext.SaveChangesAsync();

			result = Result<TransactionRequestModel>.Success(model);
		}
		catch (Exception ex)
		{
			result = Result<TransactionRequestModel>.ValidationError(ex.Message);
		}

		return result;
	}
	#endregion

}
