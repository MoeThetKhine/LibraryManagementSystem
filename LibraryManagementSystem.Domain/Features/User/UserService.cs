﻿namespace LibraryManagementSystem.Domain.Features.User;

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
				.FirstOrDefaultAsync(u => u.Email == loginUser.Email && u.Password == loginUser.Password && u.IsActive && u.IsLocked);

			if(user is null)
			{
				result = Result<LoginUserModel>.ValidationError("Invalid email or password.");
			}

			user.IsLocked = false;
			await _appDbContext.SaveChangesAsync();

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
			#region Validation

			var existingUser = await _appDbContext.TblUsers
				.FirstOrDefaultAsync(u => u.Email == userRequest.Email);

			if (existingUser is not null)
			{
				return Result<UserRequestModel>.ValidationError("A user with this email already exists.");
			}

			#endregion

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

	#region Get Member Async

	public async Task<Result<IEnumerable<UserModel>>> GetMemberAsync()
	{
		Result<IEnumerable<UserModel>> result;

		try
		{

			#region Validation

			var members = await _appDbContext.TblUsers
				.Where(u => u.UserRole == "Member" && u.IsActive)
				.ToListAsync();

			if (members is null || !members.Any())
			{
				return Result<IEnumerable<UserModel>>.ValidationError("No members found.");
			}

			#endregion

			var lst = members.Select(x => new UserModel
			{
				UserName = x.UserName,
				Email = x.Email,
				PhoneNumber = x.PhoneNumber,
				Address = x.Address,
			}).ToList();

			result = Result<IEnumerable<UserModel>>.Success(lst);
		}
		catch (Exception ex)
		{
			result = Result<IEnumerable<UserModel>>.SystemError($"An error occurred: {ex.Message}");
		}

		return result;
	}

	#endregion

	#region User Logout Async

	public async Task<Result<LogoutModel>> UserLogoutAsync(LogoutModel logoutModel)
	{
		Result<LogoutModel> result;

		try
		{

			#region Validation

			var user = await _appDbContext.TblUsers
				.FirstOrDefaultAsync(u => u.Email == logoutModel.Email  && !u.IsLocked && u.IsActive);

			if (user is null)
			{
				return Result<LogoutModel>.ValidationError("User not found.");
			}

			#endregion

			user.IsLocked = true;
			await _appDbContext.SaveChangesAsync();

			result = Result<LogoutModel>.Success(logoutModel, "User logged out successfully.");
		}
		catch (Exception ex)
		{
			result = Result<LogoutModel>.SystemError($"An error occurred: {ex.Message}");
		}

		return result;
	}

	#endregion

	#region Update User Profile Async

	public async Task<Result<UpdateUserProfile>> UpdateUserProfileAsync(string email , UpdateUserProfile updateUserProfile)
	{
		Result<UpdateUserProfile> result;

		try
		{
			var user = await _appDbContext.TblUsers
				.FirstOrDefaultAsync(u => u.Email == email && u.UserRole == "Member" && u.IsActive && !u.IsLocked);

			if (user is null)
			{
				return Result<UpdateUserProfile>.ValidationError("User not found.");
			}

			#region Validation

			if (!string.IsNullOrEmpty(updateUserProfile.UserName))
			{
				user.UserName = updateUserProfile.UserName;
			}

			if(!string.IsNullOrEmpty(updateUserProfile.Password))
			{
				user.Password = updateUserProfile.Password;
			}	

			if(!string.IsNullOrEmpty(updateUserProfile.PhoneNumber))
			{
				user.PhoneNumber = updateUserProfile.PhoneNumber;
			}

			if (!string.IsNullOrEmpty(updateUserProfile.Address))
			{
				user.Address = updateUserProfile.Address;
			}

			#endregion

			_appDbContext.Entry(user).State = EntityState.Modified;
			await _appDbContext.SaveChangesAsync();

			result = Result<UpdateUserProfile>.Success(updateUserProfile, "User profile updated successfully.");
		}
		catch (Exception ex)
		{
			result = Result<UpdateUserProfile>.SystemError($"An error occurred: {ex.Message}");
		}

		return result;
	}

	#endregion

}
