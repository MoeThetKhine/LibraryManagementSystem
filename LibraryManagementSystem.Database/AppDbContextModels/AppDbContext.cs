namespace LibraryManagementSystem.Database.AppDbContextModels;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

	#region DbSet

	public virtual DbSet<TblBook> TblBooks { get; set; }

    public virtual DbSet<TblBorrow> TblBorrows { get; set; }

    public virtual DbSet<TblCategory> TblCategories { get; set; }

    public virtual DbSet<TblReturn> TblReturns { get; set; }

    public virtual DbSet<TblTransaction> TblTransactions { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

	#endregion



	protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.;Database=LibraryManagementSystem;User Id=sa;Password=sasa@123;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblBook>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PK__Tbl_Book__3DE0C207F94EE4EA");

            entity.ToTable("Tbl_Book");

            entity.Property(e => e.BookId).HasMaxLength(50);
            entity.Property(e => e.Author).HasMaxLength(255);
            entity.Property(e => e.CategoryName).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("ISBN");
            entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Title).HasMaxLength(255);
        });

        modelBuilder.Entity<TblBorrow>(entity =>
        {
            entity.HasKey(e => e.BorrowId).HasName("PK__TblBorro__4295F83FAB183494");

            entity.ToTable("Tbl_Borrow");

            entity.Property(e => e.BorrowId).HasMaxLength(50);
            entity.Property(e => e.BookId).HasMaxLength(50);
            entity.Property(e => e.BorrowDate).HasColumnType("datetime");
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.UserId).HasMaxLength(255);
        });

        modelBuilder.Entity<TblCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Tbl_Cate__19093A0B1C0155F3");

            entity.ToTable("Tbl_Category");

            entity.HasIndex(e => e.CategoryName, "UQ__Tbl_Cate__8517B2E04D64C716").IsUnique();

            entity.Property(e => e.CategoryId).HasMaxLength(50);
            entity.Property(e => e.CategoryName).HasMaxLength(255);
        });

        modelBuilder.Entity<TblReturn>(entity =>
        {
            entity.HasKey(e => e.ReturnId).HasName("PK__TblRetur__F445E9A8FA95C61E");

            entity.ToTable("Tbl_Return");

            entity.Property(e => e.ReturnId).HasMaxLength(50);
            entity.Property(e => e.BorrowId).HasMaxLength(50);
            entity.Property(e => e.Fine).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
        });

        modelBuilder.Entity<TblTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Tbl_Tran__55433A6BBC26F307");

            entity.ToTable("Tbl_Transaction");

            entity.Property(e => e.TransactionId).HasMaxLength(50);
            entity.Property(e => e.BookId).HasMaxLength(50);
            entity.Property(e => e.BorrowDate).HasColumnType("datetime");
            entity.Property(e => e.DaysLate).HasComputedColumnSql("(datediff(day,[DueDate],[ReturnDate]))", false);
            entity.Property(e => e.DueDate).HasColumnType("datetime");
            entity.Property(e => e.Fine).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.Qty).HasDefaultValue(1);
            entity.Property(e => e.ReturnDate).HasColumnType("datetime");
            entity.Property(e => e.TotalAmount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.UserName).HasMaxLength(255);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Tbl_User__1788CC4CB1537EFB");

            entity.ToTable("Tbl_User");

            entity.HasIndex(e => e.Email, "UQ__Tbl_User__A9D105344BF41190").IsUnique();

            entity.Property(e => e.UserId).HasMaxLength(50);
            entity.Property(e => e.Address).HasMaxLength(255);
            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.Password).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(15);
            entity.Property(e => e.UserName).HasMaxLength(255);
            entity.Property(e => e.UserRole).HasMaxLength(50);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
