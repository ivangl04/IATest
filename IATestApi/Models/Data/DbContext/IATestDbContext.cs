namespace IATest.Models.Data.DbContext
{
    using Microsoft.EntityFrameworkCore;

    public class IATestDbContext : DbContext, IIATestDbContext
    {
        public IATestDbContext(DbContextOptions<IATestDbContext> options)
            : base(options)
        {
        }

        public DbSet<Solicitud> Solicitud { get; set; }

        public Task<int> SaveChangesAsync()
        {
            return base.SaveChangesAsync();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.HasDefaultSchema("IATest");

            modelBuilder.Entity<Solicitud>(entity =>
            {
                entity.ToTable("Solicitud");
                entity.HasKey(id => id.IdSolicitud);
            });
        }
    }
}
