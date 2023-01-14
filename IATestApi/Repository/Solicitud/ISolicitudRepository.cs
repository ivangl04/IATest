namespace IATest.Repository.Solicitud
{
    using IATest.Models.Data;

    public interface ISolicitudRepository
    {
        Task<Solicitud> CargarSolicitud(Solicitud solicitud);

        Task<Solicitud> ActualizarSolicitud(Solicitud solicitud);

        Task<List<Solicitud>> ConsultarSolicitudes();

        Task<Solicitud> ConsultarSolicitud(Guid id);

        Task<bool> EliminarSolicitud(Guid id);
    }
}
