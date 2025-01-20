namespace LibraryManagementSystem.Domain.Features.User;

public class UserService
{
	private readonly AppDbContext _appDbContext;

	public UserService(AppDbContext appDbContext)
	{
		_appDbContext = appDbContext;
	}

	#region Login User Async

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

	#endregion

	#region Register User Async

	public async Task<Result<UserRequestModel>> RegisterUserAsync(UserRequestModel userRequest)
	{
		Result<UserRequestModel> result;

		try
		{
			var existingUser = await _appDbContext.TblUsers
				.FirstOrDefaultAsync(u => u.Email == userRequest.Email);

			if (existingUser is not null)
			{
				return Result<UserRequestModel>.ValidationError("A user with this email already exists.");
			}

			var user = new TblUser
			{
				UserId = Guid.NewGuid().ToString(),
				UserName = userRequest.UserName,
				Email = userRequest.Email,
				Password = userRequest.Password,
				UserRole = "Member",
				PhoneNumber = userRequest.PhoneNumber,
				Address = userRequest.Address,
				IsActive = true
			};

			await _appDbContext.TblUsers.AddAsync(user);
			await _appDbContext.SaveChangesAsync();

			result = Result<UserRequestModel>.Success(userRequest, "User registered successfully.");
		}
		catch (Exception ex)
		{
			result = Result<UserRequestModel>.SystemError($"An error occurred: {ex.Message}");
		}

		return result;
	}

	#endregion

}
