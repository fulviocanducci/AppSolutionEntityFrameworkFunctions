using Microsoft.EntityFrameworkCore;
namespace Models
{
    public sealed class DatabaseContext : DbContext
    {
        public DbSet<People> People { get; set; }

        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region People_Configuration
            modelBuilder.Entity<People>(o =>
            {
                o.HasKey(x => x.Id);
                o.Property(x => x.Id);

                o.Property(x => x.Name)
                    .HasMaxLength(50)
                    .IsRequired();

                o.Property(x => x.Created)
                    .IsRequired();

                o.Property(x => x.IsActive)
                    .IsRequired();

                o.Property(x => x.Salary);                    

            });
            #endregion        
        }
    }
}
