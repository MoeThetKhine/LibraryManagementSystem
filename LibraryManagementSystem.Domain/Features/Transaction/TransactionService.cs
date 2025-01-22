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
				TotalAmount = transaction.TotalAmount
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
				TotalAmount = transaction.TotalAmount
			};

			result = Result<TransactionModel>.Success(model);
		}
		catch (Exception ex)
		{
			result = Result<TransactionModel>.ValidationError(ex.Message);
		}

		return result;
	}
}
