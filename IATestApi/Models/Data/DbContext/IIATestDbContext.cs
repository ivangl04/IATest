namespace IATest.Models.Data.DbContext
{
    using Microsoft.EntityFrameworkCore;

    public interface IIATestDbContext
    {
        DbSet<Solicitud> Solicitud { get; set; }

        Task<int> SaveChangesAsync();
    }
}
