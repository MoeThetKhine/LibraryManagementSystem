namespace LibraryManagementSystem.Domain.Features.Category;

public class CategoryService
{
	private readonly AppDbContext appDbContext;

	public CategoryService(AppDbContext appDbContext)
	{
		this.appDbContext = appDbContext;
	}

	#region Create Category Async

	public async Task<Result<CategoryRequestModel>> CreateCategoryAsync(CategoryRequestModel requestModel)
	{
		Result<CategoryRequestModel> result;

		try
		{
			var category = new TblCategory
			{
				CategoryId = Guid.NewGuid().ToString(),
				CategoryName = requestModel.CategoryName,
				Description = requestModel.Description
			};

			await appDbContext.TblCategories.AddAsync(category);
			await appDbContext.SaveChangesAsync();

			result = Result<CategoryRequestModel>.Success(requestModel, "Creating Successful.");
		}

		catch(Exception ex)
		{
			result = Result<CategoryRequestModel>.ValidationError(ex.Message);
		}	

		return result;
		
	}

	#endregion

	#region Get Category Async

	public async Task<Result<IEnumerable<CategoryRequestModel>>> GetCategoryAsync()
	{
		Result<IEnumerable<CategoryRequestModel>> result;

		try
		{
			var categories = await appDbContext.TblCategories
				.AsNoTracking()
				.ToListAsync();

			if (!categories.Any())
			{
				result = Result<IEnumerable<CategoryRequestModel>>.ValidationError("No Category Found.");
			}

			var categoryModels = categories.Select(category => new CategoryRequestModel
			{
				CategoryName = category.CategoryName,
				Description = category.Description
			}).ToList();

			result = Result<IEnumerable<CategoryRequestModel>>.Success(categoryModels, "Categories found.");
		}
		catch (Exception ex)
		{
			result =  Result<IEnumerable<CategoryRequestModel>>.SystemError($"An unexpected error occurred: {ex.Message}");
		}

		return result;
	}

	#endregion

}
