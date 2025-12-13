using ElectronicMenuDataAccess.Entities;
using ElectronicMenuDataAccess.Entity;
using Microsoft.EntityFrameworkCore;

namespace ElectronicMenuDataAccess.Context;


 public class ElectronicMenuDbContext : DbContext
    {
        // DbSets
        public DbSet<UserRoleEntity> Roles { get; set; }
        public DbSet<CategoryDishEntity> CategoriesDishes { get; set; }
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<DishEntity> Dishes { get; set; }
        public DbSet<StatusEntity> Statuses { get; set; }
        public DbSet<OrderEntity> Orders { get; set; }
        public DbSet<OrderItemEntity> OrderItems { get; set; }

        public ElectronicMenuDbContext(DbContextOptions<ElectronicMenuDbContext> options) 
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 1. BaseEntity настройки (для всех таблиц)
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entityType.ClrType))
                {
                    var entity = modelBuilder.Entity(entityType.ClrType);
                    entity.Property<Guid>("ExternalId").HasDefaultValueSql("gen_random_uuid()");
                    entity.Property<DateTime>("CreationTime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                    entity.Property<DateTime>("ModificationTime").HasDefaultValueSql("CURRENT_TIMESTAMP");
                }
            }

            // 2. Roles
            modelBuilder.Entity<UserRoleEntity>(entity =>
            {
                entity.ToTable("Roles");
                entity.Property(e => e.RoleName)
                    .IsRequired()
                    .HasMaxLength(12);
            });

            // 3. CategoriesDishes
            modelBuilder.Entity<CategoryDishEntity>(entity =>
            {
                entity.ToTable("CategoriesDishes");
                entity.Property(e => e.CategoryName)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            // 4. Users
            modelBuilder.Entity<UserEntity>(entity =>
            {
                entity.ToTable("Users", tb =>
                {
                    tb.HasCheckConstraint("CK_Users_RegistrationDate", 
                        "\"RegistrationDate\" > '2023-01-01 00:00:00'::timestamp"); 
                });
                
                entity.Property(e => e.PasswordHash)
                    .IsRequired()
                    .HasMaxLength(100);
                    
                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(50);
                    
                entity.Property(e => e.SecondName)
                    .IsRequired()
                    .HasMaxLength(50);
                    
                entity.Property(e => e.LastName)
                    .HasMaxLength(50);
                
                entity.Property(u => u.Email).HasMaxLength(100);
                
                entity.HasOne(u => u.UserRoleEntity)
                    .WithMany(r => r.Users)
                    .HasForeignKey(u => u.FKRole)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            // 5. Dishes
            modelBuilder.Entity<DishEntity>(entity =>
            {
                entity.ToTable("Dishes");
                
                entity.Property(e => e.DishName)
                    .IsRequired()
                    .HasMaxLength(30);
                    
                entity.Property(d => d.Price).HasPrecision(10, 2);
                
                entity.HasOne(d => d.CategoryDishEntity)
                    .WithMany(c => c.Dishes)
                    .HasForeignKey(d => d.FKCategory)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            // 6. Statuses
            modelBuilder.Entity<StatusEntity>(entity =>
            {
                entity.ToTable("Statuses");
                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(12);
            });

            // 7. Orders
            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.ToTable("Orders", tb =>
                {
                    tb.HasCheckConstraint("CK_Orders_OrderDate", "\"OrderDate\" > '2023-01-01 00:00:00'::timestamp");
                    tb.HasCheckConstraint("CK_Orders_TotalAmount", "\"TotalAmount\" > 0"); 
                });
                
                entity.HasOne(o => o.StatusEntity)
                    .WithMany(s => s.Orders)
                    .HasForeignKey(o => o.FKStatus)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
                
                entity.HasOne(o => o.UserEntity)
                    .WithMany(u => u.Orders)
                    .HasForeignKey(o => o.FKUser)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            // 8. OrderItems (Composite PK)
            modelBuilder.Entity<OrderItemEntity>(entity =>
            {
                entity.ToTable("OrderItems", tb =>
                {
                    tb.HasCheckConstraint("CK_OrderItems_Quantity", "\"Quantity\" > 0");     
                    tb.HasCheckConstraint("CK_OrderItems_UnitPrice", "\"UnitPrice\" >= 0");  
                });
                
                entity.HasKey(oi => new { oi.FKOrder, oi.FKDish });
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.UnitPrice).IsRequired();
                
                entity.HasOne(oi => oi.OrderEntity)
                    .WithMany(o => o.OrderItems)
                    .HasForeignKey(oi => oi.FKOrder)
                    .OnDelete(DeleteBehavior.Cascade)
                    .IsRequired();
                
                entity.HasOne(oi => oi.DishEntity)
                    .WithMany(d => d.OrderItems)
                    .HasForeignKey(oi => oi.FKDish)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        // 9. Автоматическое обновление времени (SaveChanges)
        public override int SaveChanges()
        {
            UpdateModificationTimes();
            return base.SaveChanges();
        }

        public override int SaveChanges(bool acceptAllChangesOnSuccess)
        {
            UpdateModificationTimes();
            return base.SaveChanges(acceptAllChangesOnSuccess);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            UpdateModificationTimes();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void UpdateModificationTimes()
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.ModificationTime = DateTime.UtcNow;
                        break;
                    case EntityState.Added:
                        entry.Entity.ExternalId = Guid.NewGuid();
                        entry.Entity.CreationTime = DateTime.UtcNow;
                        entry.Entity.ModificationTime = DateTime.UtcNow;
                        break;
                }
            }
        }
    }