using LibraryManagementSystem.Domain.Models.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Features.Transaction
{
	public class TransactionService
	{
		private readonly AppDbContext _appDbContext;

		public TransactionService(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

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
	}
}
