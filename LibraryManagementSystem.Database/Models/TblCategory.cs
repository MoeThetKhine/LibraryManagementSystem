using System;
using System.Collections.Generic;

namespace LibraryManagementSystem.Database.Models;

public partial class TblCategory
{
    public string CategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;

    public string? Description { get; set; }
}
