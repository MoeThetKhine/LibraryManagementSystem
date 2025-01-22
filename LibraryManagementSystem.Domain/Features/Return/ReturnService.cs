using LibraryManagementSystem.Domain.Models.Return;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryManagementSystem.Domain.Features.Return;

public class ReturnService
{
	private readonly AppDbContext _appDbContext;

	public ReturnService(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

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
}
