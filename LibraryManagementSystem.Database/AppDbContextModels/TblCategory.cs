namespace LibraryManagementSystem.Database.AppDbContextModels;

#region TblCategory

public partial class TblCategory
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }
}

#endregion