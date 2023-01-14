namespace IATest.Services.Solicitud
{
    using IATest.Models.Internal;

    public interface ISolicitudService
    {
        Task<Solicitud> CargarSolicitud(Solicitud solicitud, bool estado);

        Task<Solicitud> ActualizarSolicitud(Solicitud solicitud);

        Task<List<Solicitud>> ConsultarSolicitudes();

        Task<Solicitud> ConsultarSolicitud(Guid id);

        Task<bool> EliminarSolicitud(Guid id);
    }
}
