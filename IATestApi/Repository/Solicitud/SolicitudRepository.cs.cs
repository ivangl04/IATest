namespace IATest.Repository.Solicitud
{
    using Microsoft.EntityFrameworkCore;
    using IATest.Models.Data;
    using IATest.Models.Data.DbContext;

    public class SolicitudRepository : ISolicitudRepository
    {
        private readonly IIATestDbContext iaTestDbContext;

        public SolicitudRepository(IIATestDbContext iaTestDbContext)
        {
            this.iaTestDbContext = iaTestDbContext;
        }

        public async Task<Solicitud> ActualizarSolicitud(Solicitud solicitud)
        {
            try
            {
                var solicitudToUpdate = await this.iaTestDbContext.Solicitud.FirstAsync(s => s.IdSolicitud == solicitud.IdSolicitud);
                solicitudToUpdate.Nombre = solicitud.Nombre;
                solicitudToUpdate.Apellido = solicitud.Apellido;
                solicitudToUpdate.Identificacion = solicitud.Identificacion;
                solicitudToUpdate.Edad = solicitud.Edad;
                solicitudToUpdate.AfinidadMagica = solicitud.AfinidadMagica;
                solicitudToUpdate.Estado = solicitud.Estado;
                solicitudToUpdate.Grimorio = solicitud.Grimorio;

                await this.iaTestDbContext.SaveChangesAsync();

                return solicitudToUpdate;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Solicitud> CargarSolicitud(Solicitud solicitud)
        {
            try
            {
                await this.iaTestDbContext.Solicitud.AddAsync(solicitud);
                await this.iaTestDbContext.SaveChangesAsync();
                return solicitud;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Solicitud> ConsultarSolicitud(Guid id)
        {
            try
            {
                return await this.iaTestDbContext.Solicitud.FirstOrDefaultAsync(s => s.IdSolicitud == id);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<Solicitud>> ConsultarSolicitudes()
        {
            try
            {
                return await this.iaTestDbContext.Solicitud.ToListAsync<Solicitud>();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> EliminarSolicitud(Guid id)
        {
            try
            {
                var solicitudDeleted = await this.iaTestDbContext.Solicitud.FirstAsync(s => s.IdSolicitud == id);
                this.iaTestDbContext.Solicitud.Remove(solicitudDeleted);
                await this.iaTestDbContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
