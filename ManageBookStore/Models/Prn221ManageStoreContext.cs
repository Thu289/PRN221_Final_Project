using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ManageBookStore.Models;

public partial class Prn221ManageStoreContext : DbContext
{
    public Prn221ManageStoreContext()
    {
    }

    public Prn221ManageStoreContext(DbContextOptions<Prn221ManageStoreContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<OrderCartImport> OrderCartImports { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Post> Posts { get; set; }

    public virtual DbSet<ProductAcc> ProductAccs { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<Sale> Sales { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=cuchu;uid=sa;pwd=1234;database=PRN221_ManageStore;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Account>(entity =>
        {
            entity.ToTable("Account");

            entity.Property(e => e.Avatar)
                .IsUnicode(false)
                .HasColumnName("avatar");
            entity.Property(e => e.Email)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("email");
            entity.Property(e => e.FullName)
                .HasMaxLength(100)
                .HasColumnName("fullName");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.Password)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("password");
            entity.Property(e => e.Phone)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("phone");
            entity.Property(e => e.RemoteIp)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("remoteIp");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");
            entity.Property(e => e.UserName)
                .HasMaxLength(100)
                .HasColumnName("userName");

            entity.HasMany(d => d.Roles).WithMany(p => p.Accs)
                .UsingEntity<Dictionary<string, object>>(
                    "RoleAcc",
                    r => r.HasOne<Role>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoleAcc_Role"),
                    l => l.HasOne<Account>().WithMany()
                        .HasForeignKey("AccId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_RoleAcc_Account"),
                    j =>
                    {
                        j.HasKey("AccId", "RoleId");
                        j.ToTable("RoleAcc");
                        j.IndexerProperty<int>("AccId").HasColumnName("accId");
                        j.IndexerProperty<int>("RoleId").HasColumnName("roleId");
                    });
        });

        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("Address");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccId).HasColumnName("accId");
            entity.Property(e => e.Address1)
                .HasMaxLength(500)
                .HasColumnName("address");

            entity.HasOne(d => d.Acc).WithMany(p => p.Addresses)
                .HasForeignKey(d => d.AccId)
                .HasConstraintName("FK_Address_Account");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Product");

            entity.ToTable("Book");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasColumnType("ntext")
                .HasColumnName("description");
            entity.Property(e => e.Image)
                .HasColumnType("text")
                .HasColumnName("image");
            entity.Property(e => e.IsInStock).HasColumnName("isInStock");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");

            entity.HasMany(d => d.Posts).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookInPost",
                    r => r.HasOne<Post>().WithMany()
                        .HasForeignKey("PostId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookInPost_Posts"),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_BookInPost_Book"),
                    j =>
                    {
                        j.HasKey("BookId", "PostId");
                        j.ToTable("BookInPost");
                        j.IndexerProperty<int>("BookId").HasColumnName("bookId");
                        j.IndexerProperty<int>("PostId").HasColumnName("postId");
                    });
        });

        modelBuilder.Entity<Category>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Code)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("code");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(500)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");

            entity.HasMany(d => d.Products).WithMany(p => p.Categories)
                .UsingEntity<Dictionary<string, object>>(
                    "CategoryProduct",
                    r => r.HasOne<Book>().WithMany()
                        .HasForeignKey("ProductId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CategoryProduct_Product"),
                    l => l.HasOne<Category>().WithMany()
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("FK_CategoryProduct_Categories"),
                    j =>
                    {
                        j.HasKey("CategoryId", "ProductId");
                        j.ToTable("CategoryProduct");
                    });
        });

        modelBuilder.Entity<OrderCartImport>(entity =>
        {
            entity.ToTable("OrderCartImport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AccId).HasColumnName("accId");
            entity.Property(e => e.AddressId).HasColumnName("addressId");
            entity.Property(e => e.CreatedTime)
                .HasColumnType("datetime")
                .HasColumnName("createdTime");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.FinalPrice).HasColumnName("finalPrice");
            entity.Property(e => e.RemoteIp)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("remoteIp");
            entity.Property(e => e.ShippingFees).HasColumnName("shippingFees");
            entity.Property(e => e.Status)
                .HasMaxLength(200)
                .HasColumnName("status");
            entity.Property(e => e.TotalPrice).HasColumnName("totalPrice");
            entity.Property(e => e.Type)
                .HasMaxLength(200)
                .HasColumnName("type");

            entity.HasOne(d => d.Acc).WithMany(p => p.OrderCartImports)
                .HasForeignKey(d => d.AccId)
                .HasConstraintName("FK_OrderCartImport_Account");

            entity.HasOne(d => d.Address).WithMany(p => p.OrderCartImports)
                .HasForeignKey(d => d.AddressId)
                .HasConstraintName("FK_OrderCartImport_Address");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId });

            entity.Property(e => e.OrderId).HasColumnName("orderId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.Quantity).HasColumnName("quantity");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_OrderCartImport");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_OrderDetails_Product");
        });

        modelBuilder.Entity<Post>(entity =>
        {
            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contents)
                .HasColumnType("ntext")
                .HasColumnName("contents");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Rating).HasColumnName("rating");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .IsUnicode(false)
                .HasColumnName("status");
            entity.Property(e => e.Writer).HasColumnName("writer");

            entity.HasOne(d => d.WriterNavigation).WithMany(p => p.Posts)
                .HasForeignKey(d => d.Writer)
                .HasConstraintName("FK_Posts_Account");
        });

        modelBuilder.Entity<ProductAcc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Table_1");

            entity.Property(e => e.Id)
                .ValueGeneratedOnAdd()
                .HasColumnName("id");
            entity.Property(e => e.AccId).HasColumnName("accId");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Role)
                .HasMaxLength(200)
                .HasColumnName("role");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.ProductAcc)
                .HasForeignKey<ProductAcc>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductAccs__id__02FC7413");

            entity.HasOne(d => d.Id1).WithOne(p => p.ProductAcc)
                .HasForeignKey<ProductAcc>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__ProductAccs__id__03F0984C");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.ToTable("Rating");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Contents)
                .HasColumnType("ntext")
                .HasColumnName("contents");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.NumOfStar).HasColumnName("numOfStar");
            entity.Property(e => e.ReviewedId).HasColumnName("reviewedId");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Rating_Account");

            entity.HasOne(d => d.Reviewed).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ReviewedId)
                .HasConstraintName("FK_Rating_Book");

            entity.HasOne(d => d.ReviewedNavigation).WithMany(p => p.Ratings)
                .HasForeignKey(d => d.ReviewedId)
                .HasConstraintName("FK_Rating_Posts");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.ToTable("Report");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasColumnType("datetime")
                .HasColumnName("createdAt");
            entity.Property(e => e.Reason)
                .HasColumnType("text")
                .HasColumnName("reason");
            entity.Property(e => e.ReportedAccId).HasColumnName("reportedAccId");
            entity.Property(e => e.RepoterId).HasColumnName("repoterId");
            entity.Property(e => e.Status)
                .HasMaxLength(100)
                .HasColumnName("status");

            entity.HasOne(d => d.ReportedAcc).WithMany(p => p.ReportReportedAccs)
                .HasForeignKey(d => d.ReportedAccId)
                .HasConstraintName("FK_Report_Account1");

            entity.HasOne(d => d.Repoter).WithMany(p => p.ReportRepoters)
                .HasForeignKey(d => d.RepoterId)
                .HasConstraintName("FK_Report_Account");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.ToTable("Role");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description)
                .HasMaxLength(500)
                .HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(200)
                .HasColumnName("name");
            entity.Property(e => e.Status).HasColumnName("status");
        });

        modelBuilder.Entity<Sale>(entity =>
        {
            entity.ToTable("Sale");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy).HasColumnName("createdBy");
            entity.Property(e => e.IsActive).HasColumnName("isActive");
            entity.Property(e => e.MaxQty).HasColumnName("maxQty");
            entity.Property(e => e.MinQty).HasColumnName("minQty");
            entity.Property(e => e.NewPrice).HasColumnName("newPrice");
            entity.Property(e => e.ProductId).HasColumnName("productId");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.TimeEnd)
                .HasColumnType("datetime")
                .HasColumnName("timeEnd");
            entity.Property(e => e.TimeStart)
                .HasColumnType("datetime")
                .HasColumnName("timeStart");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.Sales)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("FK_Sale_Account");

            entity.HasOne(d => d.Product).WithMany(p => p.Sales)
                .HasForeignKey(d => d.ProductId)
                .HasConstraintName("FK_Sale_Product");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
