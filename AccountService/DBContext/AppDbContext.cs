using AccountService.Models;
using Microsoft.EntityFrameworkCore;

namespace AccountService.DBContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<ApplicationType> ApplicationTypes { get; set; }
        public DbSet<Account> Accounts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationType>(entity =>
            {
                entity.ToTable("ApplicationType");
                entity.HasKey(e => e.ApplicationId)
                      .IsClustered(true);

                entity.Property(e => e.Name)
                      .HasMaxLength(50)
                      .IsRequired();

                entity.Property(e => e.Description)
                      .HasMaxLength(255)
                      .IsRequired(false);

                entity.Property(e => e.CreateDate)
                      .IsRequired(false);

                entity.Property(e => e.UpdateDate)
                      .IsRequired(false);

                entity.Property(e=> e.UserId)
                      .IsRequired(true);

            });

            modelBuilder.Entity<Account>(entity => {
                entity.ToTable("Account");
                entity.HasKey(e => e.AccountId)
                      .IsClustered(true);

                entity.Property(e => e.UserName)
                      .HasMaxLength(100)
                      .IsRequired();

                entity.Property(e => e.PasswordHash)
                      .HasMaxLength(255)
                      .IsRequired(false);

                entity.Property(e => e.DisplayName)
                      .HasMaxLength(100)
                      .IsRequired();

                // Relationship 1 ApplicationType -> N Account
                entity.HasOne(e => e.ApplicationType)
                      .WithMany(t => t.Accounts)
                      .HasForeignKey(a => a.ApplicationTypeId)
                      .OnDelete(DeleteBehavior.Restrict);
            });
        }
    }
}
