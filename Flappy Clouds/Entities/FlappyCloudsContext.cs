using Microsoft.EntityFrameworkCore;

namespace Flappy_Clouds.Entities;

public partial class FlappyCloudsContext : DbContext
{
    public FlappyCloudsContext()
    {
    }

    public FlappyCloudsContext(DbContextOptions<FlappyCloudsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<ShoppingCart> ShoppingCarts { get; set; }

    public virtual DbSet<Subscriber> Subscribers { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=FlappyClouds;Integrated Security=True;Pooling=False;Encrypt=True;Trust Server Certificate=True");
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PK__Categori__19093A0B0E534BBB");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PK__Orders__C3905BCF9C85D547");

            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.OrderStatus).HasDefaultValue("Pending");

            entity.HasOne(d => d.User).WithMany(p => p.Orders).HasConstraintName("FK__Orders__UserId__4316F928");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => e.OrderDetailId).HasName("PK__OrderDet__D3B9D36C54E199C8");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails).HasConstraintName("FK__OrderDeta__Order__45F365D3");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails).HasConstraintName("FK__OrderDeta__Produ__46E78A0C");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PK__Products__B40CC6CDCD558F85");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.StockQuantity).HasDefaultValue(0);

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("FK__Products__Catego__3D5E1FD2");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.ReviewId).HasName("PK__Reviews__74BC79CEB33718D9");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Product).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__Product__4CA06362");

            entity.HasOne(d => d.User).WithMany(p => p.Reviews).HasConstraintName("FK__Reviews__UserId__4BAC3F29");
        });

        modelBuilder.Entity<ShoppingCart>(entity =>
        {
            entity.HasKey(e => e.CartId).HasName("PK__Shopping__51BCD7B7C96A6A6A");

            entity.Property(e => e.AddedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Quantity).HasDefaultValue(1);

            entity.HasOne(d => d.Product).WithMany(p => p.ShoppingCarts).HasConstraintName("FK__ShoppingC__Produ__52593CB8");

            entity.HasOne(d => d.User).WithMany(p => p.ShoppingCarts).HasConstraintName("FK__ShoppingC__UserI__5165187F");
        });

        modelBuilder.Entity<Subscriber>(entity =>
        {
            entity.HasKey(e => e.SubscriberId).HasName("PK__Subscrib__7DFEB6D4CC6DD233");

            entity.Property(e => e.SubscribedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__Users__1788CC4CB984D8EC");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.Role).HasDefaultValue("Customer");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
