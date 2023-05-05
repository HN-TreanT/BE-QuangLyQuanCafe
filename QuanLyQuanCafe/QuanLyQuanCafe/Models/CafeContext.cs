using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace QuanLyQuanCafe.Models
{
    public partial class CafeContext : IdentityDbContext<ApplicationUser>
    {
        public CafeContext()
        {
        }

        public CafeContext(DbContextOptions<CafeContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Account> Accounts { get; set; } = null!;
        public virtual DbSet<Category> Categories { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<DetailImportGood> DetailImportGoods { get; set; } = null!;
        public virtual DbSet<Material> Materials { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<SelectedWorkShift> SelectedWorkShifts { get; set; } = null!;
        public virtual DbSet<TableFood> TableFoods { get; set; } = null!;
        public virtual DbSet<TokenInfo> TokenInfo { get; set; } = null!;
        public virtual DbSet<UseMaterial> UseMaterials { get; set; } = null!;
      
        public virtual DbSet<WorkShift> WorkShifts { get; set; } = null!;
        public virtual DbSet<staff> staff { get; set; } = null!;

        /*protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Data Source=HN-TreanT;Initial Catalog=Cafe;User ID=sa;Password=hnam23012002");
            }
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IdentityUserLogin<string>>()
              .HasKey(l => new { l.LoginProvider, l.ProviderKey });
            modelBuilder.Entity<IdentityUserRole<string>>()
             .HasKey(ur => new { ur.UserId, ur.RoleId });
            modelBuilder.Entity<IdentityUserToken<string>>()
              .HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            modelBuilder.Entity<Account>(entity =>
            {
                entity.ToTable("account");

                entity.Property(e => e.Id)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id")
                    .IsFixedLength();

                entity.Property(e => e.Active)
                    .HasColumnName("active")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .HasColumnName("displayName")
                    .HasDefaultValueSql("('admin')");

                entity.Property(e => e.Password).HasColumnName("password");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.HasKey(e => e.IdCategory)
                    .HasName("PK__category__79D361B6AF4F7F78");

                entity.ToTable("category");

                entity.Property(e => e.IdCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idCategory")
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.IdCustomer)
                    .HasName("PK__customer__D058768637199FBB");

                entity.ToTable("customer");

                entity.Property(e => e.IdCustomer)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idCustomer")
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender)
                    .HasMaxLength(16)
                    .HasColumnName("gender");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(16)
                    .HasColumnName("phone_number");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<DetailImportGood>(entity =>
            {
                entity.HasKey(e => e.IdDetailImportGoods)
                    .HasName("PK__detailIm__8C8889A9EFF1495D");

                entity.ToTable("detailImportGoods");

                entity.Property(e => e.IdDetailImportGoods)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idDetailImportGoods")
                    .IsFixedLength();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMaterial)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_material")
                    .IsFixedLength();

                entity.Property(e => e.PhoneProvider).HasMaxLength(20);

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdMaterialNavigation)
                    .WithMany(p => p.DetailImportGoods)
                    .HasForeignKey(d => d.IdMaterial)
                    .HasConstraintName("FK_detailImportGoods_Material");
            });

            modelBuilder.Entity<Material>(entity =>
            {
                entity.HasKey(e => e.IdMaterial);

                entity.ToTable("Material");

                entity.Property(e => e.IdMaterial)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Amount)
                    .HasColumnName("amount")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasMaxLength(400);

                entity.Property(e => e.Expiry).HasColumnName("expiry");

                entity.Property(e => e.NameMaterial).HasMaxLength(50);

                entity.Property(e => e.Unit)
                    .HasMaxLength(10)
                    .HasColumnName("unit");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.IdOrder)
                    .HasName("PK__orders__C8AAF6FF62CB1FB7");

                entity.ToTable("orders");

                entity.Property(e => e.IdOrder)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idOrder")
                    .IsFixedLength();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdCustomer)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_customer")
                    .IsFixedLength();

                entity.Property(e => e.IdTable)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_table")
                    .IsFixedLength();

                entity.Property(e => e.Price)
                    .HasColumnName("price")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.TimePay).HasColumnType("datetime");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdCustomerNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdCustomer)
                    .HasConstraintName("FK__orders__id_custo__5AEE82B9");

                entity.HasOne(d => d.IdTableNavigation)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.IdTable)
                    .HasConstraintName("FK__orders__id_table__5BE2A6F2");
            });

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => e.IdOrderDetail)
                    .HasName("PK__order_de__D04A4263C154FAE8");

                entity.ToTable("order_detail");

                entity.Property(e => e.IdOrderDetail)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idOrderDetail")
                    .IsFixedLength();

                entity.Property(e => e.Amout).HasColumnName("amout");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdOrder)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_order")
                    .IsFixedLength();

                entity.Property(e => e.IdProduct)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_product")
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdOrderNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.IdOrder)
                    .HasConstraintName("FK__order_det__id_or__60A75C0F");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.OrderDetails)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK__order_det__id_pr__619B8048");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.IdProduct)
                    .HasName("PK__product__5EEC79D1CE06E0A2");

                entity.ToTable("product");

                entity.Property(e => e.IdProduct)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idProduct")
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description)
                    .HasColumnType("text")
                    .HasColumnName("description");

                entity.Property(e => e.IdCategory)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_category")
                    .IsFixedLength();

                entity.Property(e => e.Price).HasColumnName("price");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.Thumbnail)
                    .HasMaxLength(500)
                    .HasColumnName("thumbnail");

                entity.Property(e => e.Title)
                    .HasMaxLength(50)
                    .HasColumnName("title");

                entity.Property(e => e.Unit)
                    .HasMaxLength(20)
                    .HasColumnName("unit");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdCategoryNavigation)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdCategory)
                    .HasConstraintName("FK__product__id_cate__48CFD27E");
            });

            modelBuilder.Entity<SelectedWorkShift>(entity =>
            {
                entity.HasKey(e => e.IdSeletedWorkShift);

                entity.ToTable("SelectedWorkShift");

                entity.Property(e => e.IdSeletedWorkShift)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.IdStaff)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idStaff")
                    .IsFixedLength();

                entity.HasOne(d => d.IdStaffNavigation)
                    .WithMany(p => p.SelectedWorkShifts)
                    .HasForeignKey(d => d.IdStaff)
                    .HasConstraintName("FK_SelectedWorkShift_staff");

                entity.HasOne(d => d.IdWorkShiftNavigation)
                    .WithMany(p => p.SelectedWorkShifts)
                    .HasForeignKey(d => d.IdWorkShift)
                    .HasConstraintName("FK_SelectedWorkShift_WorkShift");
            });

            modelBuilder.Entity<TableFood>(entity =>
            {
                entity.HasKey(e => e.IdTable)
                    .HasName("PK__tableFoo__716BDE24A16D201C");

                entity.ToTable("tableFood");

                entity.Property(e => e.IdTable)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idTable")
                    .IsFixedLength();

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Name).HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((0))");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            modelBuilder.Entity<TokenInfo>(entity =>
            {
                entity.ToTable("TokenInfo");
            });

            modelBuilder.Entity<UseMaterial>(entity =>
            {
                entity.HasKey(e => e.IdUseMaterial);

                entity.ToTable("UseMaterial");

                entity.Property(e => e.IdUseMaterial)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .IsFixedLength();

                entity.Property(e => e.Amount).HasColumnName("amount");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.IdMaterial)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_material")
                    .IsFixedLength();

                entity.Property(e => e.IdProduct)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("id_product")
                    .IsFixedLength();

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.IdMaterialNavigation)
                    .WithMany(p => p.UseMaterials)
                    .HasForeignKey(d => d.IdMaterial)
                    .HasConstraintName("FK_UseMaterial_Material");

                entity.HasOne(d => d.IdProductNavigation)
                    .WithMany(p => p.UseMaterials)
                    .HasForeignKey(d => d.IdProduct)
                    .HasConstraintName("FK_UseMaterial_product");
            });

            modelBuilder.Entity<UserLogin>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            });

            modelBuilder.Entity<UserRole>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
            });

            modelBuilder.Entity<UserToken>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            });

            modelBuilder.Entity<WorkShift>(entity =>
            {
                entity.HasKey(e => e.IdWorkShift);

                entity.ToTable("WorkShift");

                entity.Property(e => e.IdWorkShift).ValueGeneratedNever();
            });

            modelBuilder.Entity<staff>(entity =>
            {
                entity.HasKey(e => e.IdStaff)
                    .HasName("PK__staff__98C886A9CB905920");

                entity.Property(e => e.IdStaff)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("idStaff")
                    .IsFixedLength();

                entity.Property(e => e.Address)
                    .HasMaxLength(200)
                    .HasColumnName("address");

                entity.Property(e => e.Birthday)
                    .HasColumnType("date")
                    .HasColumnName("birthday");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(150)
                    .HasColumnName("email");

                entity.Property(e => e.Fullname)
                    .HasMaxLength(50)
                    .HasColumnName("fullname");

                entity.Property(e => e.Gender)
                    .HasMaxLength(16)
                    .HasColumnName("gender");

                entity.Property(e => e.PathImage).HasColumnName("pathImage");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(16)
                    .HasColumnName("phone_number");

                entity.Property(e => e.Salary).HasColumnName("salary");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("datetime")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("(getdate())");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
