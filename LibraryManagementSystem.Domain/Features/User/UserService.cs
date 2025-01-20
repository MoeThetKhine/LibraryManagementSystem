using LibraryManagementSystem.Database.AppDbContextModels;
using LibraryManagementSystem.Domain.Models.User;
using LibraryManagementSystem.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace LibraryManagementSystem.Domain.Features.User
{
	public class UserService
	{
		private readonly AppDbContext _appDbContext;

		public UserService(AppDbContext appDbContext)
		{
			_appDbContext = appDbContext;
		}

		public async Task<Result<LoginUserModel>> LoginUserAsync(LoginUserModel loginUser)
		{
			Result<LoginUserModel> result;

			try
			{
				var user = await _appDbContext.TblUsers
					.FirstOrDefaultAsync(u => u.Email == loginUser.Email && u.Password == loginUser.Password && !u.IsActive);

				if(user is null)
				{
					result = Result<LoginUserModel>.ValidationError("Invalid email or password.");
				}

				result = Result<LoginUserModel>.Success(loginUser, "Login Successful.");
			}
			catch (Exception ex)
			{
				result = Result<LoginUserModel>.ValidationError(ex.Message);
			}

			return result;
		}

	}
}
