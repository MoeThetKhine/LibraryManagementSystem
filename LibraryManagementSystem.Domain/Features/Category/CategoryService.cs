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





}
