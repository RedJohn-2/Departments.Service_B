using Departments.Service_B.Persistence.Entities;
using Microsoft.EntityFrameworkCore;

namespace Departments.Service_B.Persistence
{
    public class ApplicationContext : DbContext
    {
        public DbSet<DepartmentEntity> Departments { get; set; } = null!;

        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<DepartmentEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.HasOne(e => e.Parent)
                      .WithMany(e => e.Children)
                      .HasForeignKey(e => e.ParentId)
                      .IsRequired(false);
            });
        }
    }
}
